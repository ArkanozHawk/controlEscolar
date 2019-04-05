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
    public partial class Calificaciones456 : MaterialForm
    {
        double calificacion;
        string Español,Matematicas,Ingless,CienciasN,Geografia,Historia,FormacionCiv,Artess,Edsocio,EducacionF,Inasistencias;
        string materia, mes;

        conexion obj = new conexion();

        public Calificaciones456()
        {
            InitializeComponent();
            validaCalifMen();
        }

        public static void ThreadProc()
        {
            Application.Run(new login());
        }

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public static void ThreadGenerarBoletas()
        {
            
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

        private void btnPrincipal_Click_1(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnIrBoletas_Click(object sender, EventArgs e)
        {
            //System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadGenerarBoletas));
            //pantalla.Start();
            //CheckForIllegalCrossThreadCalls = false;
            //this.Close();

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
            string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = "+IdAlumno+" AND `Mes` = 'Septiembre'";

            MySqlConnection conn1;
            MySqlCommand com1;

            conn1 = new MySqlConnection(conexion);
            conn1.Open();

            com1 = new MySqlCommand(CalifSep, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();

            double[] CalifSept = new double[11];
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

            double[] CalifOctu = new double[11];
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

            double[] CalifNovi = new double[11];
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

            double[] CalifDici = new double[11];
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

            double[] CalifEner = new double[11];
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

            double[] CalifFebr = new double[11];
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

            double[] CalifMarz = new double[11];
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

            double[] CalifAbri = new double[11];
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

            double[] CalifMayo = new double[11];
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

            double[] CalifJuni = new double[11];
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

            double[] CalifDiag = new double[11];
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
            FileStream stream = new FileStream(folderPath + "calificaciones456.pdf", FileMode.Create);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Boleta interna");
            doc.AddCreator("equipo master");

            // Abrimos el archivo
            doc.Open();

            //Tipos de letras para textos
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

            PdfPCell cell44 = new PdfPCell(new Phrase("GRADO : " + sesion.grado + "      " + "GRUPO:  A"));
            cell44.Colspan = 22;//toma columnas
            cell44.BorderWidth = 0;
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell42 = new PdfPCell(new Phrase("N° DE LISTA:        CICLO ESCOLAR: 2018 - 2019"));
            cell42.Colspan = 22;//toma columnas
            cell42.BorderWidth = 0;
            cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell42);

            PdfPCell cell43 = new PdfPCell(new Phrase("ALUMNO:   " + Apellidop + "     " + Apellidom + "     " + nombre + "     CURP:" + sesion.Curp));
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
            table.AddCell(""+CalifSept[0]);
            table.AddCell(""+ CalifOctu[0]);
            table.AddCell(""+ CalifNovi[0]);
            table.AddCell(""+ CalifDici[0]);
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

            PdfPCell cell6 = new PdfPCell(new Phrase("CIENCIAS NATURALES", cuerpo));
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

            PdfPCell cell50 = new PdfPCell(new Phrase("GEOGRAFÍA", cuerpo));
            cell50.Colspan = 2;
            table.AddCell(cell50);

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

            PdfPCell cell52 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
            cell52.Colspan = 2;
            table.AddCell(cell52);

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


            PdfPCell cell51 = new PdfPCell(new Phrase("FORMACIÓN CÍVICA Y ÉTICA", cuerpo));
            cell51.Colspan = 2;
            table.AddCell(cell51);

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

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

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

            table.AddCell("" + CalifDiag[8]);
            table.AddCell("" + CalifSept[8]);
            table.AddCell("" + CalifOctu[8]);
            table.AddCell("" + CalifNovi[8]);
            table.AddCell("" + CalifEner[8]);
            table.AddCell("" + CalifFebr[8]);
            table.AddCell("" + CalifMarz[8]);
            table.AddCell("" + CalifAbri[8]);
            table.AddCell("" + CalifMayo[8]);
            table.AddCell("" + CalifJuni[8]);

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

            table.AddCell("" + CalifDiag[9]);
            table.AddCell("" + CalifSept[9]);
            table.AddCell("" + CalifOctu[9]);
            table.AddCell("" + CalifNovi[9]);
            table.AddCell("" + CalifEner[9]);
            table.AddCell("" + CalifFebr[9]);
            table.AddCell("" + CalifMarz[9]);
            table.AddCell("" + CalifAbri[9]);
            table.AddCell("" + CalifMayo[9]);
            table.AddCell("" + CalifJuni[9]);

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

            table.AddCell("" + CalifDiag[9]);
            table.AddCell("" + CalifSept[9]);
            table.AddCell("" + CalifOctu[9]);
            table.AddCell("" + CalifNovi[9]);
            table.AddCell("" + CalifEner[9]);
            table.AddCell("" + CalifFebr[9]);
            table.AddCell("" + CalifMarz[9]);
            table.AddCell("" + CalifAbri[9]);
            table.AddCell("" + CalifMayo[9]);
            table.AddCell("" + CalifJuni[9]);

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

            table.AddCell("" + CalifDiag[10]);
            table.AddCell("" + CalifSept[10]);
            table.AddCell("" + CalifOctu[10]);
            table.AddCell("" + CalifNovi[10]);
            table.AddCell("" + CalifEner[10]);
            table.AddCell("" + CalifFebr[10]);
            table.AddCell("" + CalifMarz[10]);
            table.AddCell("" + CalifAbri[10]);
            table.AddCell("" + CalifMayo[10]);
            table.AddCell("" + CalifJuni[10]);
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

       

        public void calisep()
        {

            Español = EspDiag.SelectedItem.ToString();
            Matematicas = EspSep.SelectedItem.ToString();
            Ingless = EspOct.SelectedItem.ToString();
            CienciasN = Espnov.SelectedItem.ToString();
            Geografia = Espdic.SelectedItem.ToString();
            Historia = Espene.SelectedItem.ToString();
            FormacionCiv = EspFeb.SelectedItem.ToString();
            Artess = EspMarz.SelectedItem.ToString();
            EducacionF = Espmay.SelectedItem.ToString();
            Edsocio = Espabril.SelectedItem.ToString();
            Inasistencias=cmbsepina.SelectedItem.ToString();
            //Historia = Espene.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();

     
            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria();  insertarcali(); 
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); 
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); 
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); 
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
           
        }


        public void caliOct()
        {
            Español = cmbEspañol.SelectedItem.ToString();
            Matematicas = cmbOctubreMate.SelectedItem.ToString();
            Ingless = cmbOctubreIngles.SelectedItem.ToString();
            CienciasN = cmbOctubreCiencias.SelectedItem.ToString();
            Geografia = cmbOctubreGeografia.SelectedItem.ToString();
            Historia = cmbOctubreHistoria.SelectedItem.ToString();
            FormacionCiv = cmbOctubreFormacion.SelectedItem.ToString();
            Artess = cmboctubreArt.SelectedItem.ToString();
            EducacionF = cmbOctubreEdFisica.SelectedItem.ToString();
            Edsocio = cmbOctubreEdsocio.SelectedItem.ToString();
            Inasistencias = cmbOctubreIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
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
            CienciasN = cmbNovCiencias.SelectedItem.ToString();
            Geografia = cmbNovGeografia.SelectedItem.ToString();
            Historia = cmbNovHistoria.SelectedItem.ToString();
            FormacionCiv = cmbNovFormacion.SelectedItem.ToString();
            Artess = cmbNovArtes.SelectedItem.ToString();
            EducacionF = cmbNovEdFisi.SelectedItem.ToString();
            Edsocio = cmbNovEdsocio.SelectedItem.ToString();
            Inasistencias = cmbNovIna.SelectedItem.ToString();
            
            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
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
            CienciasN = cmbDicCiencias.SelectedItem.ToString();
            Geografia = cmbDicGeografia.SelectedItem.ToString();
            Historia = cmbDicHistoria.SelectedItem.ToString();
            FormacionCiv = cmbDicForm.SelectedItem.ToString();
            Artess = cmbDicArtes.SelectedItem.ToString();
            EducacionF = cmbDicEdFisica.SelectedItem.ToString();
            Edsocio = cmbDicEdsocio.SelectedItem.ToString();
            Inasistencias = cmbDicInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
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
            CienciasN = cmbEneroCiencias.SelectedItem.ToString();
            Geografia = cmbEneroGeografia.SelectedItem.ToString();
            Historia = cmbEneroHistoria.SelectedItem.ToString();
            FormacionCiv = cmbEneroFormacion.SelectedItem.ToString();
            Artess = cmbEneroArtess.SelectedItem.ToString();
            EducacionF = cmbEneroEdfisica.SelectedItem.ToString();
            Edsocio = cmbEneroEdsocio.SelectedItem.ToString();
            Inasistencias = cmbEneroIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
           
        }

        public void caliFebrero()
        {
            Español = cmbFebreroEspañol.SelectedItem.ToString();
            Matematicas = cmbFebreroMate.SelectedItem.ToString();
            Ingless = cmbfebreroIngles.SelectedItem.ToString();
            CienciasN = cmbFebreroCiencias.SelectedItem.ToString();
            Geografia = cmbFebreroGeo.SelectedItem.ToString();
            Historia = cmbFebreroHistoria.SelectedItem.ToString();
            FormacionCiv = cmbFebreroFormacion.SelectedItem.ToString();
            Artess = cmbFebreroArtess.SelectedItem.ToString();
            EducacionF = cmbFebreroEdfisica.SelectedItem.ToString();
            Edsocio = cmbFebreroEdsocio.SelectedItem.ToString();
            Inasistencias = cmbfebreroIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        private void Calificaciones456_Load(object sender, EventArgs e)
        {
            validaCalifMen();
        }

        public void caliMarzo()
        {
            
            Español = cmbmarzoEspañol.SelectedItem.ToString();
            Matematicas = cmbMarzoMate.SelectedItem.ToString();
            Ingless = cmbMarzoIngles.SelectedItem.ToString();
            CienciasN = cmbMarzoCiencias.SelectedItem.ToString();
            Geografia = cmbMarzoGeografia.SelectedItem.ToString();
            Historia = cmbMarzoHistoria.SelectedItem.ToString();
            FormacionCiv = cmbMarzoFormacion.SelectedItem.ToString();
            Artess = cmbMarzoArtess.SelectedItem.ToString();
            EducacionF = cmbMarzoEdFisica.SelectedItem.ToString();
            Edsocio = cmbMarzoEdsocio.SelectedItem.ToString();
            Inasistencias = cmbmarzoina.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliAbril()
        {
            Español = cmbAbrilEspañol.SelectedItem.ToString();
            Matematicas = cmbAbrilmate.SelectedItem.ToString();
            Ingless = cmbAbrilIngles.SelectedItem.ToString();
            CienciasN = cmbAbrilCiencias.SelectedItem.ToString();
            Geografia = cmbAbrilGeografia.SelectedItem.ToString();
            Historia = cmbAbrilHistoria.SelectedItem.ToString();
            FormacionCiv = cmbAbrilFormacion.SelectedItem.ToString();
            Artess = cmbAbrilArtess.SelectedItem.ToString();
            EducacionF = cmbAbrilEdfisica.SelectedItem.ToString();
            Edsocio = cmbAbrilEdsocio.SelectedItem.ToString();
            Inasistencias = cmbAbrilIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }
        public void caliMayo()
        {
            Español = cmbmayoEspañol.SelectedItem.ToString();
            Matematicas = cmbMayoMate.SelectedItem.ToString();
            Ingless = cmbMayoIngles.SelectedItem.ToString();
            CienciasN = cmbMayoCiencias.SelectedItem.ToString();
            Geografia = cmbMayoGeografia.SelectedItem.ToString();
            Historia = cmbMayohistoria.SelectedItem.ToString();
            FormacionCiv = cmbMayoFormacion.SelectedItem.ToString();
            Artess =cmbMayoArtes.SelectedItem.ToString();
            Edsocio = cmbMayoEdsocio.SelectedItem.ToString();
            EducacionF = cmbMayoEdfisica.SelectedItem.ToString();
            Inasistencias = cmbMayoIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliJunio()
        {
            Español = cmbDJunioEspañol.SelectedItem.ToString();
            Matematicas = cmbJuniomate.SelectedItem.ToString();
            Ingless = cmbJunioingless.SelectedItem.ToString();
            CienciasN = cmbJunioingless.SelectedItem.ToString();
            Geografia = cmbJunioGeofgrafia.SelectedItem.ToString();
            Historia = cmbJuniohistorias.SelectedItem.ToString();
            FormacionCiv = cmbJunioFormacionCivica.SelectedItem.ToString();
            Artess = cmbJunioArtess.SelectedItem.ToString();
            EducacionF = cmbJunioEdFis.SelectedItem.ToString();
            Edsocio = cmbJunioEdsocioe.SelectedItem.ToString();
            Inasistencias = cmbJunioinasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); 
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        public void caliDiagnostico()
        {
            Español = cmbdiagespañol.SelectedItem.ToString();
            Matematicas = cmbdiagmate.SelectedItem.ToString();
            Ingless = cmbdiagingles.SelectedItem.ToString();
            CienciasN = cmbdiagciencias.SelectedItem.ToString();
            Geografia = cmbdiaggeografia.SelectedItem.ToString();
            Historia = cmbdiagHistoria.SelectedItem.ToString();
            FormacionCiv = cmbdiagformacion.SelectedItem.ToString();
            Artess = cmbdiagartes.SelectedItem.ToString();
            EducacionF = cmbdiagedfisica.SelectedItem.ToString();
            Edsocio = cmbdiagedsocio.SelectedItem.ToString();
            Inasistencias = cmbdiaginasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali();
            materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        //--------------------------------------------------Validaciones-------------------------------------------
        public void validaCalifMen()
        {
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

            string[] Calificaciones = new string[300];
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
                string diag_CiNat = Calificaciones[3];
                string diag_Geo = Calificaciones[4];
                string diag_Hist = Calificaciones[5];
                string diag_FCE = Calificaciones[6];
                string diag_Artes = Calificaciones[7];
                string diag_Socio = Calificaciones[8];
                string diag_EdFis = Calificaciones[9];
                string diag_Inasis = Calificaciones[10];
                // SEPTIEMBRE
                string sept_Esp = Calificaciones[11];
                string sept_Mat = Calificaciones[12];
                string sept_Ing = Calificaciones[13];
                string sept_CiNat = Calificaciones[14];
                string sept_Geo = Calificaciones[15];
                string sept_Hist = Calificaciones[16];
                string sept_FCE = Calificaciones[17];
                string sept_Artes = Calificaciones[18];
                string sept_Socio = Calificaciones[19];
                string sept_EdFis = Calificaciones[20];
                string sept_Inasis = Calificaciones[21];
                // OCTUBRE
                string oct_Esp = Calificaciones[22];
                string oct_Mat = Calificaciones[23];
                string oct_Ing = Calificaciones[24];
                string oct_CiNat = Calificaciones[25];
                string oct_Geo = Calificaciones[26];
                string oct_Hist = Calificaciones[27];
                string oct_FCE = Calificaciones[28];
                string oct_Artes = Calificaciones[29];
                string oct_Socio = Calificaciones[30];
                string oct_EdFis = Calificaciones[31];
                string oct_Inasis = Calificaciones[32];
                // NOVIEMBRE
                string nov_Esp = Calificaciones[33];
                string nov_Mat = Calificaciones[34];
                string nov_Ing = Calificaciones[35];
                string nov_CiNat = Calificaciones[36];
                string nov_Geo = Calificaciones[37];
                string nov_Hist = Calificaciones[38];
                string nov_FCE = Calificaciones[39];
                string nov_Artes = Calificaciones[40];
                string nov_Socio = Calificaciones[41];
                string nov_EdFis = Calificaciones[42];
                string nov_Inasis = Calificaciones[43];
                // DICIEMBRE
                string dic_Esp = Calificaciones[44];
                string dic_Mat = Calificaciones[45];
                string dic_Ing = Calificaciones[46];
                string dic_CiNat = Calificaciones[47];
                string dic_Geo = Calificaciones[48];
                string dic_Hist = Calificaciones[49];
                string dic_FCE = Calificaciones[50];
                string dic_Artes = Calificaciones[51];
                string dic_Socio = Calificaciones[52];
                string dic_EdFis = Calificaciones[53];
                string dic_Inasis = Calificaciones[54];
                // ENERO
                string ene_Esp = Calificaciones[55];
                string ene_Mat = Calificaciones[56];
                string ene_Ing = Calificaciones[57];
                string ene_CiNat = Calificaciones[58];
                string ene_Geo = Calificaciones[59];
                string ene_Hist = Calificaciones[60];
                string ene_FCE = Calificaciones[61];
                string ene_Artes = Calificaciones[62];
                string ene_Socio = Calificaciones[63];
                string ene_EdFis = Calificaciones[64];
                string ene_Inasis = Calificaciones[65];
                // FEBRERO
                string feb_Esp = Calificaciones[66];
                string feb_Mat = Calificaciones[67];
                string feb_Ing = Calificaciones[68];
                string feb_CiNat = Calificaciones[69];
                string feb_Geo = Calificaciones[70];
                string feb_Hist = Calificaciones[71];
                string feb_FCE = Calificaciones[72];
                string feb_Artes = Calificaciones[73];
                string feb_Socio = Calificaciones[74];
                string feb_EdFis = Calificaciones[75];
                string feb_Inasis = Calificaciones[76];
                // MARZO
                string mar_Esp = Calificaciones[77];
                string mar_Mat = Calificaciones[78];
                string mar_Ing = Calificaciones[79];
                string mar_CiNat = Calificaciones[80];
                string mar_Geo = Calificaciones[81];
                string mar_Hist = Calificaciones[82];
                string mar_FCE = Calificaciones[83];
                string mar_Artes = Calificaciones[84];
                string mar_Socio = Calificaciones[85];
                string mar_EdFis = Calificaciones[86];
                string mar_Inasis = Calificaciones[87];
                // ABRIL
                string abr_Esp = Calificaciones[88];
                string abr_Mat = Calificaciones[89];
                string abr_Ing = Calificaciones[90];
                string abr_CiNat = Calificaciones[91];
                string abr_Geo = Calificaciones[92];
                string abr_Hist = Calificaciones[93];
                string abr_FCE = Calificaciones[94];
                string abr_Artes = Calificaciones[95];
                string abr_Socio = Calificaciones[96];
                string abr_EdFis = Calificaciones[97];
                string abr_Inasis = Calificaciones[98];
                // MAYO
                string may_Esp = Calificaciones[99];
                string may_Mat = Calificaciones[100];
                string may_Ing = Calificaciones[101];
                string may_CiNat = Calificaciones[102];
                string may_Geo = Calificaciones[103];
                string may_Hist = Calificaciones[104];
                string may_FCE = Calificaciones[105];
                string may_Artes = Calificaciones[106];
                string may_Socio = Calificaciones[107];
                string may_EdFis = Calificaciones[108];
                string may_Inasis = Calificaciones[109];
                // JUNIO
                string jun_Esp = Calificaciones[110];
                string jun_Mat = Calificaciones[111];
                string jun_Ing = Calificaciones[112];
                string jun_CiNat = Calificaciones[113];
                string jun_Geo = Calificaciones[114];
                string jun_Hist = Calificaciones[115];
                string jun_FCE = Calificaciones[116];
                string jun_Artes = Calificaciones[117];
                string jun_Socio = Calificaciones[118];
                string jun_EdFis = Calificaciones[119];
                string jun_Inasis = Calificaciones[120];


                // AGREGADO SEPTIEMBRE
                EspDiag.Text = sept_Esp;
                EspSep.Text = sept_Mat;
                EspOct.Text = sept_Ing;
                Espnov.Text = sept_CiNat;
                Espdic.Text = sept_Geo;
                Espene.Text = sept_Hist;
                EspFeb.Text = sept_FCE;
                EspMarz.Text = sept_Artes;
                Espabril.Text = sept_Socio;
                Espmay.Text = sept_EdFis;
                cmbsepina.Text = sept_Inasis;
                // AGREGADO OCTUBRE
                cmbEspañol.Text = oct_Esp;
                cmbOctubreMate.Text = oct_Mat;
                cmbOctubreIngles.Text = oct_Ing;
                cmbOctubreCiencias.Text = oct_CiNat;
                cmbOctubreGeografia.Text = oct_Geo;
                cmbOctubreHistoria.Text = oct_Hist;
                cmbOctubreFormacion.Text = oct_FCE;
                cmboctubreArt.Text = oct_Artes;
                cmbOctubreEdsocio.Text = oct_Socio;
                cmbOctubreEdFisica.Text = oct_EdFis;
                cmbOctubreIna.Text = oct_Inasis;
                // AGREGADO NOVIEMBRE
                cmbNovEspañol.Text = nov_Esp;
                cmbNovmate.Text = nov_Mat;
                cmbNovIngles.Text = nov_Ing;
                cmbNovCiencias.Text = nov_CiNat;
                cmbNovGeografia.Text = nov_Geo;
                cmbNovHistoria.Text = nov_Hist;
                cmbNovFormacion.Text = nov_FCE;
                cmbNovArtes.Text = nov_Artes;
                cmbNovEdsocio.Text = nov_Socio;
                cmbNovEdFisi.Text = nov_EdFis;
                cmbNovIna.Text = nov_Inasis;
                // AGREGADO DICIEMBRE
                cmbDicEspañol.Text = dic_Esp;
                cmbDicMate.Text = dic_Mat;
                cmbDicIngles.Text = dic_Ing;
                cmbDicCiencias.Text = dic_CiNat;
                cmbDicGeografia.Text = dic_Geo;
                cmbDicHistoria.Text = dic_Hist;
                cmbDicForm.Text = dic_FCE;
                cmbDicArtes.Text = dic_Artes;
                cmbDicEdsocio.Text = dic_Socio;
                cmbDicEdFisica.Text = dic_EdFis;
                cmbDicInasis.Text = dic_Inasis;
                // AGREGADO ENERO
                cmbEneroEspañol.Text = ene_Esp;
                cmbEneroMate.Text = ene_Mat;
                cmbEneroIngles.Text = ene_Ing;
                cmbEneroCiencias.Text = ene_CiNat;
                cmbEneroGeografia.Text = ene_Geo;
                cmbEneroHistoria.Text = ene_Hist;
                cmbEneroFormacion.Text = ene_FCE;
                cmbEneroArtess.Text = ene_Artes;
                cmbEneroEdsocio.Text = ene_Socio;
                cmbEneroEdfisica.Text = ene_EdFis;
                cmbEneroIna.Text = ene_Inasis;
                // AGREGADO FEBRERO
                cmbFebreroEspañol.Text = feb_Esp;
                cmbFebreroMate.Text = feb_Mat;
                cmbfebreroIngles.Text = feb_Ing;
                cmbFebreroCiencias.Text = feb_CiNat;
                cmbFebreroGeo.Text = feb_Geo;
                cmbFebreroHistoria.Text = feb_Hist;
                cmbFebreroFormacion.Text = feb_FCE;
                cmbFebreroArtess.Text = feb_Artes;
                cmbFebreroEdsocio.Text = feb_Socio;
                cmbFebreroEdfisica.Text = feb_EdFis;
                cmbfebreroIna.Text = feb_Inasis;
                // AGREGADO MARZO
                cmbmarzoEspañol.Text = mar_Esp;
                cmbMarzoMate.Text = mar_Mat;
                cmbMarzoIngles.Text = mar_Ing;
                cmbMarzoCiencias.Text = mar_CiNat;
                cmbMarzoGeografia.Text = mar_Geo;
                cmbMarzoHistoria.Text = mar_Hist;
                cmbMarzoFormacion.Text = mar_FCE;
                cmbMarzoArtess.Text = mar_Artes;
                cmbMarzoEdsocio.Text = mar_Socio;
                cmbMarzoEdFisica.Text = mar_EdFis;
                cmbmarzoina.Text = mar_Inasis;
                // AGREGADO ABRIL
                cmbAbrilEspañol.Text = abr_Esp;
                cmbAbrilmate.Text = abr_Mat;
                cmbAbrilIngles.Text = abr_Ing;
                cmbAbrilCiencias.Text = abr_CiNat;
                cmbAbrilGeografia.Text = abr_Geo;
                cmbAbrilHistoria.Text = abr_Hist;
                cmbAbrilFormacion.Text = abr_FCE;
                cmbAbrilArtess.Text = abr_Artes;
                cmbAbrilEdsocio.Text = abr_Socio;
                cmbAbrilEdfisica.Text = abr_EdFis;
                cmbAbrilIna.Text = abr_Inasis;
                // AGREGADO MAYO
                cmbmayoEspañol.Text = may_Esp;
                cmbMayoMate.Text = may_Mat;
                cmbMayoIngles.Text = may_Ing;
                cmbMayoCiencias.Text = may_CiNat;
                cmbMayoGeografia.Text = may_Geo;
                cmbMayohistoria.Text = may_Hist;
                cmbMayoFormacion.Text = may_FCE;
                cmbMayoArtes.Text = may_Artes;
                cmbMayoEdsocio.Text = may_Socio;
                cmbMayoEdfisica.Text = may_EdFis;
                cmbMayoIna.Text = may_Inasis;
                // AGREGADO JUNIO
                cmbDJunioEspañol.Text = jun_Esp;
                cmbJuniomate.Text = jun_Mat;
                cmbJunioingless.Text = jun_Ing;
                cmbJunioCienciass.Text = jun_CiNat;
                cmbJunioGeofgrafia.Text = jun_Geo;
                cmbJuniohistorias.Text = jun_Hist;
                cmbJunioFormacionCivica.Text = jun_FCE;
                cmbJunioArtess.Text = jun_Artes;
                cmbJunioEdsocioe.Text = jun_Socio;
                cmbJunioEdFis.Text = jun_EdFis;
                cmbJunioinasis.Text = jun_Inasis;
                // AGREGADO DIAGNOSTICO
                cmbdiagespañol.Text = diag_Esp;
                cmbdiagmate.Text = diag_Mat;
                cmbdiagingles.Text = diag_Ing;
                cmbdiagciencias.Text = diag_CiNat;
                cmbdiaggeografia.Text = diag_Geo;
                cmbdiagHistoria.Text = diag_Hist;
                cmbdiagformacion.Text = diag_FCE;
                cmbdiagartes.Text = diag_Artes;
                cmbdiagedsocio.Text = diag_Socio;
                cmbdiagedfisica.Text = diag_EdFis;
                cmbdiaginasis.Text = diag_Inasis;


                // BLOQUEO SEPTIEMBRE
                if (EspDiag.Text != "")
                {
                    EspDiag.Enabled = false;
                }
                if (EspSep.Text != "")
                {
                    EspSep.Enabled = false;
                }
                if (EspOct.Text != "")
                {
                    EspOct.Enabled = false;
                }
                if (Espnov.Text != "")
                {
                    Espnov.Enabled = false;
                }
                if (Espdic.Text != "")
                {
                    Espdic.Enabled = false;
                }
                if (Espene.Text != "")
                {
                    Espene.Enabled = false;
                }
                if (EspFeb.Text != "")
                {
                    EspFeb.Enabled = false;
                }
                if (EspMarz.Text != "")
                {
                    EspMarz.Enabled = false;
                }
                if (Espabril.Text != "")
                {
                    Espabril.Enabled = false;
                }
                if (Espmay.Text != "")
                {
                    Espmay.Enabled = false;
                }
                if (cmbsepina.Text != "")
                {
                    cmbsepina.Enabled = false;
                }

                // BLOQUEO OCTUBRE
                if (cmbEspañol.Text != "")
                {
                    cmbEspañol.Enabled = false;
                }
                if (cmbOctubreMate.Text != "")
                {
                    cmbOctubreMate.Enabled = false;
                }
                if (cmbOctubreIngles.Text != "")
                {
                    cmbOctubreIngles.Enabled = false;
                }
                if (cmbOctubreCiencias.Text != "")
                {
                    cmbOctubreCiencias.Enabled = false;
                }
                if (cmbOctubreGeografia.Text != "")
                {
                    cmbOctubreGeografia.Enabled = false;
                }
                if (cmbOctubreHistoria.Text != "")
                {
                    cmbOctubreHistoria.Enabled = false;
                }
                if (cmbOctubreFormacion.Text != "")
                {
                    cmbOctubreFormacion.Enabled = false;
                }
                if (cmboctubreArt.Text != "")
                {
                    cmboctubreArt.Enabled = false;
                }
                if (cmbOctubreEdsocio.Text != "")
                {
                    cmbOctubreEdsocio.Enabled = false;
                }
                if (cmbOctubreEdFisica.Text != "")
                {
                    cmbOctubreEdFisica.Enabled = false;
                }
                if (cmbOctubreIna.Text != "")
                {
                    cmbOctubreIna.Enabled = false;
                }

                // BLOQUEO NOVIEMBRE
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
                if (cmbNovCiencias.Text != "")
                {
                    cmbNovCiencias.Enabled = false;
                }
                if (cmbNovGeografia.Text != "")
                {
                    cmbNovGeografia.Enabled = false;
                }
                if (cmbNovHistoria.Text != "")
                {
                    cmbNovHistoria.Enabled = false;
                }
                if (cmbNovFormacion.Text != "")
                {
                    cmbNovFormacion.Enabled = false;
                }
                if (cmbNovArtes.Text != "")
                {
                    cmbNovArtes.Enabled = false;
                }
                if (cmbNovEdsocio.Text != "")
                {
                    cmbNovEdsocio.Enabled = false;
                }
                if (cmbNovEdFisi.Text != "")
                {
                    cmbNovEdFisi.Enabled = false;
                }
                if (cmbNovIna.Text != "")
                {
                    cmbNovIna.Enabled = false;
                }

                // BLOQUEO DICIEMBRE
                if (cmbDicEspañol.Text != "")
                {
                    cmbDicEspañol.Enabled = false;
                }
                if (cmbDicMate.Text != "")
                {
                    cmbDicMate.Enabled = false;
                }
                if (cmbDicIngles.Text != "")
                {
                    cmbDicIngles.Enabled = false;
                }
                if (cmbDicCiencias.Text != "")
                {
                    cmbDicCiencias.Enabled = false;
                }
                if (cmbDicGeografia.Text != "")
                {
                    cmbDicGeografia.Enabled = false;
                }
                if (cmbDicHistoria.Text != "")
                {
                    cmbDicHistoria.Enabled = false;
                }
                if (cmbDicForm.Text != "")
                {
                    cmbDicForm.Enabled = false;
                }
                if (cmbDicArtes.Text != "")
                {
                    cmbDicArtes.Enabled = false;
                }
                if (cmbDicEdsocio.Text != "")
                {
                    cmbDicEdsocio.Enabled = false;
                }
                if (cmbDicEdFisica.Text != "")
                {
                    cmbDicEdFisica.Enabled = false;
                }
                if (cmbDicInasis.Text != "")
                {
                    cmbDicInasis.Enabled = false;
                }

                // BLOQUEO ENERO
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
                if (cmbEneroCiencias.Text != "")
                {
                    cmbEneroCiencias.Enabled = false;
                }
                if (cmbEneroGeografia.Text != "")
                {
                    cmbEneroGeografia.Enabled = false;
                }
                if (cmbEneroHistoria.Text != "")
                {
                    cmbEneroHistoria.Enabled = false;
                }
                if (cmbEneroFormacion.Text != "")
                {
                    cmbEneroFormacion.Enabled = false;
                }
                if (cmbEneroArtess.Text != "")
                {
                    cmbEneroArtess.Enabled = false;
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

                // BLOQUEO FEBRERO
                if (cmbFebreroEspañol.Text != "")
                {
                    cmbFebreroEspañol.Enabled = false;
                }
                if (cmbFebreroMate.Text != "")
                {
                    cmbFebreroMate.Enabled = false;
                }
                if (cmbfebreroIngles.Text != "")
                {
                    cmbfebreroIngles.Enabled = false;
                }
                if (cmbFebreroCiencias.Text != "")
                {
                    cmbFebreroCiencias.Enabled = false;
                }
                if (cmbFebreroGeo.Text != "")
                {
                    cmbFebreroGeo.Enabled = false;
                }
                if (cmbFebreroHistoria.Text != "")
                {
                    cmbFebreroHistoria.Enabled = false;
                }
                if (cmbFebreroFormacion.Text != "")
                {
                    cmbFebreroFormacion.Enabled = false;
                }
                if (cmbFebreroArtess.Text != "")
                {
                    cmbFebreroArtess.Enabled = false;
                }
                if (cmbFebreroEdsocio.Text != "")
                {
                    cmbFebreroEdsocio.Enabled = false;
                }
                if (cmbFebreroEdfisica.Text != "")
                {
                    cmbFebreroEdfisica.Enabled = false;
                }
                if (cmbfebreroIna.Text != "")
                {
                    cmbfebreroIna.Enabled = false;
                }

                // BLOQUEO MARZO
                if (cmbmarzoEspañol.Text != "")
                {
                    cmbmarzoEspañol.Enabled = false;
                }
                if (cmbMarzoMate.Text != "")
                {
                    cmbMarzoMate.Enabled = false;
                }
                if (cmbMarzoIngles.Text != "")
                {
                    cmbMarzoIngles.Enabled = false;
                }
                if (cmbMarzoCiencias.Text != "")
                {
                    cmbMarzoCiencias.Enabled = false;
                }
                if (cmbMarzoGeografia.Text != "")
                {
                    cmbMarzoGeografia.Enabled = false;
                }
                if (cmbMarzoHistoria.Text != "")
                {
                    cmbMarzoHistoria.Enabled = false;
                }
                if (cmbMarzoFormacion.Text != "")
                {
                    cmbMarzoFormacion.Enabled = false;
                }
                if (cmbMarzoArtess.Text != "")
                {
                    cmbMarzoArtess.Enabled = false;
                }
                if (cmbMarzoEdsocio.Text != "")
                {
                    cmbMarzoEdsocio.Enabled = false;
                }
                if (cmbMarzoEdFisica.Text != "")
                {
                    cmbMarzoEdFisica.Enabled = false;
                }
                if (cmbmarzoina.Text != "")
                {
                    cmbmarzoina.Enabled = false;
                }

                // BLOQUEO ABRIL
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
                if (cmbAbrilCiencias.Text != "")
                {
                    cmbAbrilCiencias.Enabled = false;
                }
                if (cmbAbrilGeografia.Text != "")
                {
                    cmbAbrilGeografia.Enabled = false;
                }
                if (cmbAbrilHistoria.Text != "")
                {
                    cmbAbrilHistoria.Enabled = false;
                }
                if (cmbAbrilFormacion.Text != "")
                {
                    cmbAbrilFormacion.Enabled = false;
                }
                if (cmbAbrilArtess.Text != "")
                {
                    cmbAbrilArtess.Enabled = false;
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

                // BLOQUEO MAYO
                if (cmbmayoEspañol.Text != "")
                {
                    cmbmayoEspañol.Enabled = false;
                }
                if (cmbMayoMate.Text != "")
                {
                    cmbMayoMate.Enabled = false;
                }
                if (cmbMayoIngles.Text != "")
                {
                    cmbMayoIngles.Enabled = false;
                }
                if (cmbMayoCiencias.Text != "")
                {
                    cmbMayoCiencias.Enabled = false;
                }
                if (cmbMayoGeografia.Text != "")
                {
                    cmbMayoGeografia.Enabled = false;
                }
                if (cmbMayohistoria.Text != "")
                {
                    cmbMayohistoria.Enabled = false;
                }
                if (cmbMayoFormacion.Text != "")
                {
                    cmbMayoFormacion.Enabled = false;
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

                // BLOQUEO JUNIO
                if (cmbDJunioEspañol.Text != "")
                {
                    cmbDJunioEspañol.Enabled = false;
                }
                if (cmbJuniomate.Text != "")
                {
                    cmbJuniomate.Enabled = false;
                }
                if (cmbJunioingless.Text != "")
                {
                    cmbJunioingless.Enabled = false;
                }
                if (cmbJunioCienciass.Text != "")
                {
                    cmbJunioCienciass.Enabled = false;
                }
                if (cmbJunioGeofgrafia.Text != "")
                {
                    cmbJunioGeofgrafia.Enabled = false;
                }
                if (cmbJuniohistorias.Text != "")
                {
                    cmbJuniohistorias.Enabled = false;
                }
                if (cmbJunioFormacionCivica.Text != "")
                {
                    cmbJunioFormacionCivica.Enabled = false;
                }
                if (cmbJunioArtess.Text != "")
                {
                    cmbJunioArtess.Enabled = false;
                }
                if (cmbJunioEdsocioe.Text != "")
                {
                    cmbJunioEdsocioe.Enabled = false;
                }
                if (cmbJunioEdFis.Text != "")
                {
                    cmbJunioEdFis.Enabled = false;
                }
                if (cmbJunioinasis.Text != "")
                {
                    cmbJunioinasis.Enabled = false;
                }

                // BLOQUEO DIAGNOSTICO
                if (cmbdiagespañol.Text != "")
                {
                    cmbdiagespañol.Enabled = false;
                }
                if (cmbdiagmate.Text != "")
                {
                    cmbdiagmate.Enabled = false;
                }
                if (cmbdiagingles.Text != "")
                {
                    cmbdiagingles.Enabled = false;
                }
                if (cmbdiagciencias.Text != "")
                {
                    cmbdiagciencias.Enabled = false;
                }
                if (cmbdiaggeografia.Text != "")
                {
                    cmbdiaggeografia.Enabled = false;
                }
                if (cmbdiagHistoria.Text != "")
                {
                    cmbdiagHistoria.Enabled = false;
                }
                if (cmbdiagformacion.Text != "")
                {
                    cmbdiagformacion.Enabled = false;
                }
                if (cmbdiagartes.Text != "")
                {
                    cmbdiagartes.Enabled = false;
                }
                if (cmbdiagedsocio.Text != "")
                {
                    cmbdiagedsocio.Enabled = false;
                }
                if (cmbdiagedfisica.Text != "")
                {
                    cmbdiagedfisica.Enabled = false;
                }
                if (cmbdiaginasis.Text != "")
                {
                    cmbdiaginasis.Enabled = false;
                }

                //MessageBox.Show(sept_Esp.ToString());
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //------------------------------------Metodo para buscar la materia-----------------------------------------------
        public void buscarmateria()
        {
            mes = materialTabControl1.SelectedTab.Name;
            MySqlConnection conn;
            MySqlCommand com;
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT * FROM `materias` WHERE `nombre` = "+materia+"  AND `idGrado` = 4 ";
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
        //for ( int i = 0; i <= 11; i++)
        //{
        //    switch (i)
        //    {
        //        case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
        //        case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
        //        case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
        //        case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
        //        case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
        //        case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
        //        case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
        //        case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
        //        case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
        //        case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
        //        case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

        //    }

        //}





        public void calimat()
        {


            //diagnostico = cmbDiagMate.SelectedItem.ToString();
            //enero = cmbEneroMate.SelectedItem.ToString();
            //febrero = cmbFebreroMate.SelectedItem.ToString();
            //marzo = cmbMarzoMate.SelectedItem.ToString();
            //abril = cmbAbrilMate.SelectedItem.ToString();
            //mayo = cmbMayoMate.SelectedItem.ToString();
            //junio = cmbJunioMate.SelectedItem.ToString();
            //septiembre = cmbSeptiembreMate.SelectedItem.ToString();
            //octubre = cmbOctubreMate.SelectedItem.ToString();
            //noviembre = cmbNoviembreMate.SelectedItem.ToString();
            //diciembre = cmbDiciembreMate.SelectedItem.ToString();




            //for (int i = 0; i <= 11; i++)
            //{
            //    switch (i)
            //    {
            //        case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //        case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //        case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //        case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //        case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //        case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //        case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //        case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //        case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //        case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //        case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //    }

            //}


        }

        public void caliIngles()
        {


            // Español = cmbDiagIng.SelectedItem.ToString();
            //Historia = cmbEneroIng.SelectedItem.ToString();
            //FormacionCiv = cmbFebreroIng.SelectedItem.ToString();
            //Artess  = cmbMarzoIng.SelectedItem.ToString();
            // Edsocio = cmbAbrilIng.SelectedItem.ToString();
            //EducacionF  = cmbMayoIng.SelectedItem.ToString();
            //Matematicas = cmbSeptiembreIng.SelectedItem.ToString();
            //Ingless= cmbOctubreIng.SelectedItem.ToString();
            //Geografia  = cmbNoviembreIng.SelectedItem.ToString();
            // = cmbDiciembreIng.SelectedItem.ToString();




            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }




            //    }


            //}

            //public void caliCnat()
            //{


            //    diagnostico = cmbDiagNaturales.SelectedItem.ToString();
            //    enero = cmbEneroNaturales.SelectedItem.ToString();
            //    febrero = cmbFebreroNaturales.SelectedItem.ToString();
            //    marzo = cmbMarzoNaturales.SelectedItem.ToString();
            //    abril = cmbAbrilNaturales.SelectedItem.ToString();
            //    mayo = cmbMayoNaturales.SelectedItem.ToString();
            //    junio = cmbJunioNaturales.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreNaturales.SelectedItem.ToString();
            //    octubre = cmbOctubreNaturales.SelectedItem.ToString();
            //    noviembre = cmbNoviembreNaturales.SelectedItem.ToString();
            //    diciembre = cmbDiciembreNaturales.SelectedItem.ToString();




            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}
            //public void caliGeo()
            //{


            //    diagnostico = cmbDiagGeo.SelectedItem.ToString();
            //    enero = cmbEneroGeo.SelectedItem.ToString();
            //    febrero = cmbFebreroGeo.SelectedItem.ToString();
            //    marzo = cmbMarzoGeo.SelectedItem.ToString();
            //    abril = cmbAbrilGeo.SelectedItem.ToString();
            //    mayo = cmbMayoGeo.SelectedItem.ToString();
            //    junio = cmbJunioGeo.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreGeo.SelectedItem.ToString();
            //    octubre = cmbOctubreGeo.SelectedItem.ToString();
            //    noviembre = cmbNoviembreGeo.SelectedItem.ToString();
            //    diciembre = cmbDiciembreGeo.SelectedItem.ToString();




            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}
            //public void caliHist()
            //{


            //    diagnostico = cmbDiagHist.SelectedItem.ToString();
            //    enero = cmbEneroHist.SelectedItem.ToString();
            //    febrero = cmbFebreroHist.SelectedItem.ToString();
            //    marzo = cmbMarzoGeo.SelectedItem.ToString();
            //    abril = cmbAbrilGeo.SelectedItem.ToString();
            //    mayo = cmbMayoGeo.SelectedItem.ToString();
            //    junio = cmbJunioGeo.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreGeo.SelectedItem.ToString();
            //    octubre = cmbOctubreGeo.SelectedItem.ToString();
            //    noviembre = cmbNoviembreGeo.SelectedItem.ToString();
            //    diciembre = cmbDiciembreGeo.SelectedItem.ToString();



            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}
            //public void Formacioncivi()
            //{


            //    diagnostico = cmbDiagFCE.SelectedItem.ToString();
            //    enero = cmbEneroFCE.SelectedItem.ToString();
            //    febrero = cmbFebreroFCE.SelectedItem.ToString();
            //    marzo = cmbMarzoFCE.SelectedItem.ToString();
            //    abril = cmbAbrilFCE.SelectedItem.ToString();
            //    mayo = cmbMayoFCE.SelectedItem.ToString();
            //    junio = cmbJunioFCE.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreFCE.SelectedItem.ToString();
            //    octubre = cmbOctubreFCE.SelectedItem.ToString();
            //    noviembre = cmbNoviembreFCE.SelectedItem.ToString();
            //    diciembre = cmbDiciembreFCE.SelectedItem.ToString();



            //    for (int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}

            //public void caliartes()
            //{


            //    diagnostico = cmbDiagArtes.SelectedItem.ToString();
            //    enero = cmbEneroFCE.SelectedItem.ToString();
            //    febrero = cmbFebreroFCE.SelectedItem.ToString();
            //    marzo = cmbMarzoFCE.SelectedItem.ToString();
            //    abril = cmbAbrilFCE.SelectedItem.ToString();
            //    mayo = cmbMayoFCE.SelectedItem.ToString();
            //    junio = cmbJunioFCE.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreFCE.SelectedItem.ToString();
            //    octubre = cmbOctubreFCE.SelectedItem.ToString();
            //    noviembre = cmbNoviembreFCE.SelectedItem.ToString();
            //    diciembre = cmbDiciembreFCE.SelectedItem.ToString();




            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}
            //public void caliEdus()
            //{


            //    diagnostico = cmbDiagSocio.SelectedItem.ToString();
            //    enero = cmbEneroSocio.SelectedItem.ToString();
            //    febrero = cmbFebreroSocio.SelectedItem.ToString();
            //    marzo = cmbMarzoSocio.SelectedItem.ToString();
            //    abril = cmbAbrilSocio.SelectedItem.ToString();
            //    mayo = cmbMayoSocio.SelectedItem.ToString();
            //    junio = cmbJunioSocio.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreSocio.SelectedItem.ToString();
            //    octubre = cmbOctubreSocio.SelectedItem.ToString();
            //    noviembre = cmbNoviembreSocio.SelectedItem.ToString();
            //    diciembre = cmbDiciembreSocio.SelectedItem.ToString();




            //    for (   int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


            //}
            //public void caliEdufi()
            //{


            //    diagnostico = cmbDiagEdFis.SelectedItem.ToString();
            //    enero = cmbEneroEdFis.SelectedItem.ToString();
            //    febrero = cmbFebreroEdFis.SelectedItem.ToString();
            //    marzo = cmbMarzoEdFis.SelectedItem.ToString();
            //    abril = cmbAbrilEdFis.SelectedItem.ToString();
            //    mayo = cmbMayoEdFis.SelectedItem.ToString();
            //    junio = cmbJunioEdFis.SelectedItem.ToString();
            //    septiembre = cmbSeptiembreEdFis.SelectedItem.ToString();
            //    octubre = cmbOctubreEdFis.SelectedItem.ToString();
            //    noviembre = cmbNoviembreEdFis.SelectedItem.ToString();
            //    diciembre = cmbDiciembreEdFis.SelectedItem.ToString();




            //    for ( int i = 0; i <= 11; i++)
            //    {
            //        switch (i)
            //        {
            //            case 1: { mes = "Enero"; calificacion = Convert.ToDouble(enero); insertarcali(); } break;
            //            case 2: { mes = "Febrero"; calificacion = Convert.ToDouble(febrero); insertarcali(); } break;
            //            case 3: { mes = "marzo"; calificacion = Convert.ToDouble(marzo); insertarcali(); } break;
            //            case 4: { mes = "abril"; calificacion = Convert.ToDouble(abril); insertarcali(); } break;
            //            case 5: { mes = "mayo"; calificacion = Convert.ToDouble(mayo); insertarcali(); } break;
            //            case 6: { mes = "junio"; calificacion = Convert.ToDouble(junio); insertarcali(); } break;
            //            case 7: { mes = "septiembre"; calificacion = Convert.ToDouble(septiembre); insertarcali(); } break;
            //            case 8: { mes = "octubre"; calificacion = Convert.ToDouble(octubre); insertarcali(); } break;
            //            case 9: { mes = "noviembre"; calificacion = Convert.ToDouble(noviembre); insertarcali(); } break;
            //            case 10: { mes = " diciembre"; calificacion = Convert.ToDouble(diciembre); insertarcali(); } break;
            //            case 11: { mes = "Diagnostico"; calificacion = Convert.ToDouble(diagnostico); insertarcali(); } break;

            //        }

            //    }


        }
        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            mes = materialTabControl1.SelectedTab.Name;
            switch (mes)
            {
                case "Septiembre":
                    {
                        calisep();
                        MessageBox.Show("Calificaciones  septiembre registradas  con exito");
                    } break;

                case "Octubre":
                    {
                        caliOct();
                        MessageBox.Show("Calificaciones  octubre registradas con exito");
                    }
                    break;

                case "Noviembre":
                    {
                        caliNov();
                        MessageBox.Show("Calificaciones  noviembre registradas con exito");
                    }
                    break;

                case "Diciembre":
                    {
                        caliDic();
                        MessageBox.Show("Calificaciones  diciembre registradas con exito");
                    }
                    break;

                case "Enero":
                    {
                        caliEnero();
                        MessageBox.Show("Calificaciones  Enero registradas con exito");
                    }
                    break;
                case "Febrero":
                    {
                        caliFebrero();
                        MessageBox.Show("Calificaciones  febrero registradas con exito");
                    }
                    break;

                case "Marzo":
                    {
                        caliMarzo();
                        MessageBox.Show("Calificaciones  Marzo registradas con exito");
                    }
                    break;

                case "Abril":
                    {
                        caliAbril();
                        MessageBox.Show("Calificaciones  Abril registradas con exito");
                    }
                    break;

                case "Mayo":
                    {
                        caliMayo();
                        MessageBox.Show("Calificaciones  Mayo registradas con exito");
                    }
                    break;

                case "Junio":
                    {
                        caliJunio();
                        MessageBox.Show("Calificaciones  Junio registradas con exito");
                    }
                    break;


                case "Diagnostico":
                    {
                        caliDiagnostico();
                        MessageBox.Show("Calificaciones  Diagnostico registradas con exito");
                    }
                    break;

            }

        }
        
    }
}
