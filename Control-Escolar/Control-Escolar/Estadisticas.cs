﻿using System;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
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

            PdfPCell cell39 = new PdfPCell(new Phrase("ESTADÍSTICA BÁSICA POR CENTRO DEL TRABAJO", tituloprin));
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

            PdfPCell cell447 = new PdfPCell(new Phrase("  ", cuerpo));
            cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell447);

            PdfPCell cell427 = new PdfPCell(new Phrase(" ", cuerpo));
            cell427.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell427);

            PdfPCell cell428 = new PdfPCell(new Phrase(" ", cuerpo));
            cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell428);

            PdfPCell cell448 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell448.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell448);

            PdfPCell cell725 = new PdfPCell(new Phrase(" ", cuerpo));
            cell725.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell725);

            PdfPCell cell747 = new PdfPCell(new Phrase("  ", cuerpo));
            cell747.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell747);

            PdfPCell cell727 = new PdfPCell(new Phrase(" ", cuerpo));
            cell727.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell727);

            PdfPCell cell728 = new PdfPCell(new Phrase(" ", cuerpo));
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

            PdfPCell cell417 = new PdfPCell(new Phrase("  ", cuerpo));
            cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell417);

            PdfPCell cell437 = new PdfPCell(new Phrase(" ", cuerpo));
            cell437.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell437);

            PdfPCell cell468 = new PdfPCell(new Phrase(" ", cuerpo));
            cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell468);

            PdfPCell cell648 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell648.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell648);

            PdfPCell cell765 = new PdfPCell(new Phrase(" ", cuerpo));
            cell765.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell765);

            PdfPCell cell767 = new PdfPCell(new Phrase("  ", cuerpo));
            cell767.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell767);

            PdfPCell cell777 = new PdfPCell(new Phrase(" ", cuerpo));
            cell777.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell777);

            PdfPCell cell778 = new PdfPCell(new Phrase(" ", cuerpo));
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

            PdfPCell cell07 = new PdfPCell(new Phrase("  ", cuerpo));
            cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell07);

            PdfPCell cell071 = new PdfPCell(new Phrase(" ", cuerpo));
            cell071.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell071);

            PdfPCell cell08 = new PdfPCell(new Phrase(" ", cuerpo));
            cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell08);

            PdfPCell cell09 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell09.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell09);

            PdfPCell cell010 = new PdfPCell(new Phrase(" ", cuerpo));
            cell010.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell010);

            PdfPCell cell011 = new PdfPCell(new Phrase("  ", cuerpo));
            cell011.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell011);

            PdfPCell cell012 = new PdfPCell(new Phrase(" ", cuerpo));
            cell012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell012);

            PdfPCell cell013 = new PdfPCell(new Phrase(" ", cuerpo));
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

            PdfPCell cell017 = new PdfPCell(new Phrase("  ", cuerpo));
            cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell017);

            PdfPCell cell018 = new PdfPCell(new Phrase(" ", cuerpo));
            cell018.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell018);

            PdfPCell cell019 = new PdfPCell(new Phrase(" ", cuerpo));
            cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell019);

            PdfPCell cell020 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell020.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell020);

            PdfPCell cell021 = new PdfPCell(new Phrase(" ", cuerpo));
            cell021.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell021);

            PdfPCell cell022 = new PdfPCell(new Phrase("  ", cuerpo));
            cell022.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell022);

            PdfPCell cell023 = new PdfPCell(new Phrase(" ", cuerpo));
            cell023.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell023);

            PdfPCell cell024 = new PdfPCell(new Phrase(" ", cuerpo));
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

            PdfPCell cell037 = new PdfPCell(new Phrase("  ", cuerpo));
            cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell037);

            PdfPCell cell0373 = new PdfPCell(new Phrase(" ", cuerpo));
            cell0373.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell0373);

            PdfPCell cell038 = new PdfPCell(new Phrase(" ", cuerpo));
            cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell038);

            PdfPCell cell039 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell039.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell039);

            PdfPCell cell040 = new PdfPCell(new Phrase(" ", cuerpo));
            cell040.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell040);

            PdfPCell cell041 = new PdfPCell(new Phrase("  ", cuerpo));
            cell041.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell041);

            PdfPCell cell042 = new PdfPCell(new Phrase(" ", cuerpo));
            cell042.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell042);

            PdfPCell cell043 = new PdfPCell(new Phrase(" ", cuerpo));
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

            PdfPCell cell047 = new PdfPCell(new Phrase("  ", cuerpo));
            cell047.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell047);

            PdfPCell cell048 = new PdfPCell(new Phrase(" ", cuerpo));
            cell048.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell048);

            PdfPCell cell049 = new PdfPCell(new Phrase(" ", cuerpo));
            cell049.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell049);

            PdfPCell cell050 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell050.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell050);

            PdfPCell cell051 = new PdfPCell(new Phrase(" ", cuerpo));
            cell051.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell051);

            PdfPCell cell052 = new PdfPCell(new Phrase("  ", cuerpo));
            cell052.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell052);

            PdfPCell cell053 = new PdfPCell(new Phrase(" ", cuerpo));
            cell053.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell053);

            PdfPCell cell054 = new PdfPCell(new Phrase(" ", cuerpo));
            cell054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell054);

            PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4331.Colspan = 5;//toma columnas
            cell4331.BorderWidth = 0;
            table.AddCell(cell4331);

            PdfPCell cell244 = new PdfPCell(new Phrase("TOTAL GENERAL ", cuerpo));
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
            cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell242);

            PdfPCell cell241 = new PdfPCell(new Phrase("  ", cuerpo));
            cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell241);

            PdfPCell cell221 = new PdfPCell(new Phrase(" ", cuerpo));
            cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell221);

            PdfPCell cell226 = new PdfPCell(new Phrase(" ", cuerpo));
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
