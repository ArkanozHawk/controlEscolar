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
        public Calificaciones456()
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

        private void GroupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void CmbMarzoMate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public static void ThreadGenerarBoletas()

        {
            Application.Run(new GenerarBoletas());
        }

        private void CmbAbrilIng_SelectedIndexChanged(object sender, EventArgs e)
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

            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            string nombre, Apellidop, Apellidom;




            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();


            myreader.Read();
            nombre = Convert.ToString(myreader["nombre"]);
            Apellidop = Convert.ToString(myreader["ApellidoP"]);
            Apellidom = Convert.ToString(myreader["ApellidoM"]);
            sesion.grado = Convert.ToString(myreader["idGrado"]);

            MessageBox.Show(nombre);
            MessageBox.Show(Apellidop);
            MessageBox.Show(Apellidom);
            MessageBox.Show(sesion.Curp);
            MessageBox.Show(sesion.grado);

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

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell4 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
            cell4.Colspan = 2;
            table.AddCell(cell4);

            table.AddCell(" ");
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
            table.AddCell(" ");

            PdfPCell cell5 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
            cell5.Colspan = 2;
            table.AddCell(cell5);

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell6 = new PdfPCell(new Phrase("CIENCIAS NATURALES", cuerpo));
            cell6.Colspan = 2;
            table.AddCell(cell6);

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell50 = new PdfPCell(new Phrase("GEOGRAFÍA", cuerpo));
            cell50.Colspan = 2;
            table.AddCell(cell50);

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell52 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
            cell52.Colspan = 2;
            table.AddCell(cell52);

            table.AddCell(" ");
            table.AddCell(" ");
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


            PdfPCell cell51 = new PdfPCell(new Phrase("FORMACIÓN CÍVICA Y ÉTICA", cuerpo));
            cell51.Colspan = 2;
            table.AddCell(cell51);

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell9 = new PdfPCell(new Phrase("EDUCACIÓN FÍSICA", cuerpo));
            cell9.Colspan = 2;
            table.AddCell(cell9);

            table.AddCell(" ");
            table.AddCell(" ");
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

            table.AddCell(" ");
            table.AddCell(" ");
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

            PdfPCell cell14 = new PdfPCell(new Phrase("PROM. FINAL DE LAS ÁREAS DESARROLLO PERSONAL Y SOCIAL FORMACIÓN ACADÉMICA", letchica));
            cell14.Colspan = 2;
            cell14.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell14);

            table.AddCell(" ");
            table.AddCell(" ");
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

            table.AddCell(" ");
            table.AddCell(" ");
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
            Historia = Espene.SelectedItem.ToString();
            FormacionCiv = EspFeb.SelectedItem.ToString();
            Artess = EspMarz.SelectedItem.ToString();
            Edsocio = Espabril.SelectedItem.ToString();
            EducacionF = Espmay.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = EspSep.SelectedItem.ToString();
            Ingless = EspOct.SelectedItem.ToString();
            CienciasN = Espnov.SelectedItem.ToString();
            Geografia = Espdic.SelectedItem.ToString();
            Historia = Espene.SelectedItem.ToString();

            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria();  insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                   case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                  //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                   // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }


        public void caliOct()
        {


            Español = cmbEspañol.SelectedItem.ToString();
            Historia = cmbOctubreHistoria.SelectedItem.ToString();
            FormacionCiv = cmbOctubreFormacion.SelectedItem.ToString();
            Artess = cmboctubreArt.SelectedItem.ToString();
            Edsocio = cmbOctubreEdsocio.SelectedItem.ToString();
            EducacionF = cmbOctubreEdFisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbOctubreMate.SelectedItem.ToString();
            Ingless = cmbOctubreIngles.SelectedItem.ToString();
            CienciasN = cmbOctubreCiencias.SelectedItem.ToString();
            Geografia = cmbOctubreGeografia.SelectedItem.ToString();
            

            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                  //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }


        public void caliNov()
        {


            Español = cmbNovEspañol.SelectedItem.ToString();
            Historia = cmbNovHistoria.SelectedItem.ToString();
            FormacionCiv = cmbNovFormacion.SelectedItem.ToString();
            Artess = cmbNovArtes.SelectedItem.ToString();
            Edsocio = cmbNovEdsocio.SelectedItem.ToString();
            EducacionF = cmbNovEdFisi.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbNovmate.SelectedItem.ToString();
            Ingless = cmbNovIngles.SelectedItem.ToString();
            CienciasN = cmbNovCiencias.SelectedItem.ToString();
            Geografia = cmbNovGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }

        public void caliDic()
        {


            Español = cmbDicEspañol.SelectedItem.ToString();
            Historia = cmbDicHistoria.SelectedItem.ToString();
            FormacionCiv = cmbDicForm.SelectedItem.ToString();
            Artess = cmbDicArtes.SelectedItem.ToString();
            Edsocio = cmbDicEdsocio.SelectedItem.ToString();
            EducacionF = cmbDicEdFisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbDicMate.SelectedItem.ToString();
            Ingless = cmbDicIngles.SelectedItem.ToString();
            CienciasN = cmbDicCiencias.SelectedItem.ToString();
            Geografia = cmbDicGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }


        public void caliEnero()
        {


            Español = cmbEneroEspañol.SelectedItem.ToString();
            Historia = cmbEneroHistoria.SelectedItem.ToString();
            FormacionCiv = cmbEneroFormacion.SelectedItem.ToString();
            Artess = cmbEneroArtess.SelectedItem.ToString();
            Edsocio = cmbEneroEdsocio.SelectedItem.ToString();
            EducacionF = cmbEneroEdfisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbEneroMate.SelectedItem.ToString();
            Ingless = cmbEneroIngles.SelectedItem.ToString();
            CienciasN = cmbEneroCiencias.SelectedItem.ToString();
            Geografia = cmbEneroGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }

        public void caliFebrero()
        {


            Español = cmbFebreroEspañol.SelectedItem.ToString();
            Historia = cmbFebreroHistoria.SelectedItem.ToString();
            FormacionCiv = cmbFebreroFormacion.SelectedItem.ToString();
            Artess = cmbFebreroArtess.SelectedItem.ToString();
            Edsocio = cmbFebreroEdsocio.SelectedItem.ToString();
            EducacionF = cmbFebreroEdfisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbFebreroMate.SelectedItem.ToString();
            Ingless = cmbfebreroIngles.SelectedItem.ToString();
            CienciasN = cmbFebreroCiencias.SelectedItem.ToString();
            Geografia = cmbFebreroGeo.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }


        public void caliMarzo()
        {


            Español = cmbmarzoEspañol.SelectedItem.ToString();
            Historia = cmbMarzoHistoria.SelectedItem.ToString();
            FormacionCiv = cmbMarzoFormacion.SelectedItem.ToString();
            Artess = cmbMarzoArtess.SelectedItem.ToString();
            Edsocio = cmbMarzoEdsocio.SelectedItem.ToString();
            EducacionF = cmbMarzoEdFisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbMarzoMate.SelectedItem.ToString();
            Ingless = cmbMarzoIngles.SelectedItem.ToString();
            CienciasN = cmbMarzoCiencias.SelectedItem.ToString();
            Geografia = cmbMarzoGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }

        public void caliAbril()
        {


            Español = cmbAbrilEspañol.SelectedItem.ToString();
            Historia = cmbAbrilHistoria.SelectedItem.ToString();
            FormacionCiv = cmbAbrilFormacion.SelectedItem.ToString();
            Artess = cmbAbrilArtess.SelectedItem.ToString();
            Edsocio = cmbAbrilEdsocio.SelectedItem.ToString();
            EducacionF = cmbAbrilEdfisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbAbrilmate.SelectedItem.ToString();
            Ingless = cmbAbrilIngles.SelectedItem.ToString();
            CienciasN = cmbAbrilCiencias.SelectedItem.ToString();
            Geografia = cmbAbrilGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }
        public void caliMayo()
        {


            Español = cmbmayoEspañol.SelectedItem.ToString();
            Historia = cmbMayohistoria.SelectedItem.ToString();
            FormacionCiv = cmbMayoFormacion.SelectedItem.ToString();
            Artess =cmbMayoArtes.SelectedItem.ToString();
            Edsocio = cmbMayoEdsocio.SelectedItem.ToString();
            EducacionF = cmbMayoEdfisica.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbMayoMate.SelectedItem.ToString();
            Ingless = cmbMayoIngles.SelectedItem.ToString();
            CienciasN = cmbMayoCiencias.SelectedItem.ToString();
            Geografia = cmbMayoGeografia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }

        public void caliJunio()
        {


            Español = cmbDJunioEspañol.SelectedItem.ToString();
            Historia = cmbJuniohistorias.SelectedItem.ToString();
            FormacionCiv = cmbJunioFormacionCivica.SelectedItem.ToString();
            Artess = cmbJunioArtess.SelectedItem.ToString();
            Edsocio = cmbJunioEdsocioe.SelectedItem.ToString();
            EducacionF = cmbJunioEdFis.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();
            Matematicas = cmbJuniomate.SelectedItem.ToString();
            Ingless = cmbJunioingless.SelectedItem.ToString();
            CienciasN = cmbJunioingless.SelectedItem.ToString();
            Geografia = cmbJunioGeofgrafia.SelectedItem.ToString();


            for (int i = 0; i <= 11; i++)
            {
                switch (i)
                {
                    case 1: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;
                    case 2: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                    case 3: { materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali(); } break;
                    case 4: { materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali(); } break;
                    case 5: { materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali(); } break;
                    case 6: { materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali(); } break;
                    case 7: { materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali(); } break;
                    case 8: { materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali(); } break;
                    case 9: { materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali(); } break;
                    case 10: { materia = " 'Geografia' "; calificacion = Convert.ToDouble(Geografia); buscarmateria(); insertarcali(); } break;
                        //  case 11: { materia = " 'Historia' "; calificacion = Convert.ToDouble(Historia); buscarmateria(); insertarcali(); } break;
                        // case 12: { materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali(); } break;

                }

            }

        }
        public void buscarmateria()
        {
            mes = materialTabControl1.SelectedTab.Name;
            MySqlConnection conn;
            MySqlCommand com;
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT * FROM `materias` WHERE `nombre` = "+materia+"  AND `idGrado` = 4 ";
            MessageBox.Show(query);
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();


            myreader.Read();
            try
            {
                sesion.idmateria = Convert.ToString(myreader["idMaterias"]);

                MessageBox.Show(sesion.idmateria.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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

            }
           //calisep();
           // MessageBox.Show("Calificaciones registradas con exito");
            //foreach (TabPage page in materialTabControl1.TabPages)
            //{
            //    sesion.nombremateria = page.Name;


            //sesion.nombremateria = page.Name;





            //        MySqlConnection conn;
            //        MySqlCommand com;
            //        string conexion = "server=localhost;uid=root;database=nerivela";
            //        string query = "select * from materias where nombre like '" + sesion.nombremateria + "%' and idGradoM = " + sesion.grado + " order by materias.idMaterias ASC Limit 0, 30;";
            //        MessageBox.Show(query);
            //        conn = new MySqlConnection(conexion);
            //        conn.Open();

            //        com = new MySqlCommand(query, conn);

            //        MySqlDataReader myreader = com.ExecuteReader();


            //        myreader.Read();
            //        try
            //        {
            //            sesion.idmateria = Convert.ToString(myreader["idMaterias"]);

            //            MessageBox.Show(sesion.idmateria.ToString());
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }




            //    switch (sesion.nombremateria)
            //        {
            //            case "Esp":
            //                {
            //                    caliesp();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                }
            //                break;
            //            case "Mat":
            //                {
            //                    calimat();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                    caliIngles();
            //                }
            //                break;
            //            case "Ingles":
            //                {
            //                    caliIngles();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                }
            //                break;
            //            case "CienciasNaturales":
            //                {
            //                    caliCnat();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                }
            //                break;
            //            case "Geo":
            //                {
            //                    caliGeo();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                    caliHist();
            //                }
            //                break;
            //            case "Hist":
            //                {
            //                    caliHist();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                }
            //                break;
            //            case "FormacionCiv. y Étic":
            //                {
            //                    Formacioncivi();
            //                    MessageBox.Show("calificacion guardada con exito");
            //                }
            //              break;
            //        case "Artes":
            //            {
            //                caliartes();
            //                MessageBox.Show("calificacion guardada con exito");
            //            }
            //            break;
            //        case "Ed.socio":
            //            {
            //                caliEdus();
            //                MessageBox.Show("calificacion guardada con exito");
            //            }
            //            break;
            //        case "Ed. Física":
            //            {
            //                caliEdufi();
            //                MessageBox.Show("calificacion guardada con exito");
            //            }
            //            break;
            //            ;


            //    }

            //}
            //caliesp();
            //MessageBox.Show("calificacion guardada con exito");
            //calimat();
            //MessageBox.Show("calificacion guardada con exito");
            //caliIngles();
            //MessageBox.Show("calificacion guardada con exito");
            //caliCnat();
            //MessageBox.Show("calificacion guardada con exito");
            //caliGeo();
            //MessageBox.Show("calificacion guardada con exito");
            //caliHist();
            //MessageBox.Show("calificacion guardada con exito");
            //Formacioncivi();
            //MessageBox.Show("calificacion guardada con exito");
            //caliartes();
            //MessageBox.Show("calificacion guardada con exito");
            //caliEdus();
            //MessageBox.Show("calificacion guardada con exito");
            //caliEdufi();
            //MessageBox.Show("calificacion guardada con exito");




        }

        public void insertarcali()
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



            string conexion1 = "server=localhost;uid=root;database=nerivela";

            string inserta_bitacora = "INSERT INTO `calificaciones`( `CalificacionMen`, `idAlumno`,`Mes`, `idMaterias`) VALUES (" + calificacion + "," + idalumno + ",'" + mes + "'," + sesion.idmateria + ");";
            MessageBox.Show(inserta_bitacora);
            obj.insBitacora(conexion1, inserta_bitacora);

        }


    }
}
