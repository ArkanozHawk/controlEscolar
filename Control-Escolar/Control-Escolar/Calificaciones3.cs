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
    public partial class Calificaciones3 : MaterialForm
    {
        public Calificaciones3()
        {
            InitializeComponent();
            validaCalifMen();
            cambiacolor(groupBox1);
            cambiacolor(groupBox2);
            cambiacolor(groupBox3);
            cambiacolor(groupBox4);
            cambiacolor(groupBox5);
            cambiacolor(groupBox6);
            cambiacolor(groupBox7);
            cambiacolor(groupBox8);
            cambiacolor(groupBox9);
            cambiacolor(groupBox10);
            cambiacolor(groupBox11);
        }

        double calificacion;
        string Español, Matematicas, Ingless, CienciasN, LaEntidad, FormacionCiv, Artess, Edsocio, EducacionF, Inasistencias;
        string materia, mes;

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            mes = materialTabControl1.SelectedTab.Name;
            switch (mes)
            {
                case "Septiembre":
                    {
                        if(cmbSepInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. ¡No se puede Editar!");
                        }
                        else
                        {
                            if (cmbDiagInasis.Enabled == false)
                            {
                                if (ValidaCampos(groupBox1) == true)
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
                        if (cmbOctInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbSepInasis.Enabled == false)
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
                        if (cmbNovInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbOctInasis.Enabled == false)
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
                        if (cmbDicInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbNovInasis.Enabled == false)
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
                        if (cmbEneInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbNovInasis.Enabled == false)
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
                        if (cmbFebInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbEneInasis.Enabled == false)
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
                        if (cmbMarInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbFebInasis.Enabled == false)
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
                        if (cmbAbrInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbMarInasis.Enabled == false)
                            {
                                if (ValidaCampos(groupBox9) == true)
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
                        if (cmbMayInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbAbrInasis.Enabled == false)
                            {
                                if (ValidaCampos(groupBox8) == true)
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
                        if (cmbJunInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbMayInasis.Enabled == false)
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
                        if (cmbDiagInasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if(ValidaCampos(groupBox11) == true)
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

        conexion obj = new conexion();

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

        private void btnIrBoletas_Click_1(object sender, EventArgs e)
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
            string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Septiembre'";

            MySqlConnection conn1;
            MySqlCommand com1;

            conn1 = new MySqlConnection(conexion);
            conn1.Open();

            com1 = new MySqlCommand(CalifSep, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();

            double[] CalifSept = new double[10];
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

            double[] CalifOctu = new double[10];
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

            double[] CalifNovi = new double[10];
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

            com4 = new MySqlCommand(CalifDic, conn4);

            MySqlDataReader myreader4 = com4.ExecuteReader();

            double[] CalifDici = new double[10];
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

            double[] CalifEner = new double[10];
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

            double[] CalifFebr = new double[10];
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

            double[] CalifMarz = new double[10];
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

            double[] CalifAbri = new double[10];
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

            double[] CalifMayo = new double[10];
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

            double[] CalifJuni = new double[10];
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

            double[] CalifDiag = new double[10];
            int N = 0;
            while (myreader11.Read())
            {
                CalifDiag[N] = Convert.ToDouble(myreader11["CalificacionMen"]);
                N++;
            }

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\Boletas\Boleta-Interna3.pdf", FileMode.Create));

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

            PdfPCell cell50 = new PdfPCell(new Phrase("LA ENTIDAD DONDE VIVO", cuerpo));
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

            PdfPCell cell51 = new PdfPCell(new Phrase("FORMACIÓN CÍVICA Y ÉTICA", cuerpo));
            cell51.Colspan = 2;
            table.AddCell(cell51);

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

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

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

            PdfPCell cell9 = new PdfPCell(new Phrase("EDUCACIÓN FÍSICA", cuerpo));
            cell9.Colspan = 2;
            table.AddCell(cell9);

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

            PdfPCell cell14 = new PdfPCell(new Phrase("PROM. FINAL DE LAS ÁREAS DESARROLLO PERSONAL Y SOCIAL FORMACIÓN ACADÉMICA", letchica));
            cell14.Colspan = 2;
            cell14.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell14);

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
            foreach (Control _group in groupBox1.Controls)
            {
                // Do Something

                if (_group is ComboBox)
                {
                    ComboBox combo = new ComboBox();
                    combo.Name = _group.Name;
                    if (combo.Text == string.Empty)
                    {
                        MessageBox.Show("No se han Registrado todas las Calificaciones. Favor de llenar todos los campos.");
                    }
                    else
                    {

                    }

                }

            }

            Español = cmbSepEspañol.SelectedItem.ToString();
            Matematicas = cmbSepMatematicas.SelectedItem.ToString();
            Ingless = cmbSepIngles.SelectedItem.ToString();
            CienciasN = cmbSepCiencias.SelectedItem.ToString();
            LaEntidad = cmbSepEntidad.SelectedItem.ToString();
            FormacionCiv = cmbSepFormacion.SelectedItem.ToString();
            Artess = cmbSepArtes.SelectedItem.ToString();
            EducacionF = cmbSepEdFisica.SelectedItem.ToString();
            Edsocio = cmbSepSocio.SelectedItem.ToString();
            Inasistencias = cmbSepInasis.SelectedItem.ToString();
            //Historia = Espene.SelectedItem.ToString();
            //Inasistencias = Espjun.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliOct()
        {
            Español = cmbOctEspañol.SelectedItem.ToString();
            Matematicas = cmbOctMatematicas.SelectedItem.ToString();
            Ingless = cmbOctIngles.SelectedItem.ToString();
            CienciasN = cmbOctCiencias.SelectedItem.ToString();
            LaEntidad = cmbOctEntidad.SelectedItem.ToString();
            FormacionCiv = cmbOctFormacion.SelectedItem.ToString();
            Artess = cmbOctArtes.SelectedItem.ToString();
            EducacionF = cmbOctEdFisica.SelectedItem.ToString();
            Edsocio = cmbOctSocio.SelectedItem.ToString();
            Inasistencias = cmbOctInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliNov()
        {
            Español = cmbNovEspañol.SelectedItem.ToString();
            Matematicas = cmbNovMatematicas.SelectedItem.ToString();
            Ingless = cmbNovIngles.SelectedItem.ToString();
            CienciasN = cmbNovCiencias.SelectedItem.ToString();
            LaEntidad = cmbNovEntidad.SelectedItem.ToString();
            FormacionCiv = cmbNovFormacion.SelectedItem.ToString();
            Artess = cmbNovArtes.SelectedItem.ToString();
            EducacionF = cmbNovEdFisica.SelectedItem.ToString();
            Edsocio = cmbNovSocio.SelectedItem.ToString();
            Inasistencias = cmbNovInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliDic()
        {
            Español = cmbDicEspañol.SelectedItem.ToString();
            Matematicas = cmbDicMatematicas.SelectedItem.ToString();
            Ingless = cmbDicIngles.SelectedItem.ToString();
            CienciasN = cmbDicCiencias.SelectedItem.ToString();
            LaEntidad = cmbDicEntidad.SelectedItem.ToString();
            FormacionCiv = cmbDicFormacion.SelectedItem.ToString();
            Artess = cmbDicArtes.SelectedItem.ToString();
            EducacionF = cmbDicEdFisica.SelectedItem.ToString();
            Edsocio = cmbDicSocio.SelectedItem.ToString();
            Inasistencias = cmbDicInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliEnero()
        {
            Español = cmbEneEspañol.SelectedItem.ToString();
            Matematicas = cmbEneMatematicas.SelectedItem.ToString();
            Ingless = cmbEneIngles.SelectedItem.ToString();
            CienciasN = cmbEneCiencias.SelectedItem.ToString();
            LaEntidad = cmbEneEntidad.SelectedItem.ToString();
            FormacionCiv = cmbEneFormacion.SelectedItem.ToString();
            Artess = cmbEneArtes.SelectedItem.ToString();
            EducacionF = cmbEneEdFisica.SelectedItem.ToString();
            Edsocio = cmbEneSocio.SelectedItem.ToString();
            Inasistencias = cmbEneInasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliFebrero()
        {
            Español = cmbFebEspañol.SelectedItem.ToString();
            Matematicas = cmbFebMatematicas.SelectedItem.ToString();
            Ingless = cmbFebIngles.SelectedItem.ToString();
            CienciasN = cmbFebCiencias.SelectedItem.ToString();
            LaEntidad = cmbFebEntidad.SelectedItem.ToString();
            FormacionCiv = cmbFebFormacion.SelectedItem.ToString();
            Artess = cmbFebArtes.SelectedItem.ToString();
            EducacionF = cmbFebEdFisica.SelectedItem.ToString();
            Edsocio = cmbFebSocio.SelectedItem.ToString();
            Inasistencias = cmbFebInasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliMarzo()
        {

            Español = cmbMarEspañol.SelectedItem.ToString();
            Matematicas = cmbMarMatematicas.SelectedItem.ToString();
            Ingless = cmbMarIngles.SelectedItem.ToString();
            CienciasN = cmbMarCiencias.SelectedItem.ToString();
            LaEntidad = cmbMarEntidad.SelectedItem.ToString();
            FormacionCiv = cmbMarFormacion.SelectedItem.ToString();
            Artess = cmbMarArtes.SelectedItem.ToString();
            EducacionF = cmbMarEdFisica.SelectedItem.ToString();
            Edsocio = cmbMarSocio.SelectedItem.ToString();
            Inasistencias = cmbMarInasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliAbril()
        {
            Español = cmbAbrEspañol.SelectedItem.ToString();
            Matematicas = cmbAbrMatematicas.SelectedItem.ToString();
            Ingless = cmbAbrIngles.SelectedItem.ToString();
            CienciasN = cmbAbrCiencias.SelectedItem.ToString();
            LaEntidad = cmbAbrEntidad.SelectedItem.ToString();
            FormacionCiv = cmbAbrFormacion.SelectedItem.ToString();
            Artess = cmbAbrArtes.SelectedItem.ToString();
            EducacionF = cmbAbrEdFisica.SelectedItem.ToString();
            Edsocio = cmbAbrSocio.SelectedItem.ToString();
            Inasistencias = cmbAbrInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliMayo()
        {
            Español = cmbMayEspañol.SelectedItem.ToString();
            Matematicas = cmbMayMatematicas.SelectedItem.ToString();
            Ingless = cmbMayIngles.SelectedItem.ToString();
            CienciasN = cmbMayCiencias.SelectedItem.ToString();
            LaEntidad = cmbMayEntidad.SelectedItem.ToString();
            FormacionCiv = cmbMayFormacion.SelectedItem.ToString();
            Artess = cmbMayArtes.SelectedItem.ToString();
            Edsocio = cmbMaySocio.SelectedItem.ToString();
            EducacionF = cmbMaySocio.SelectedItem.ToString();
            Inasistencias = cmbMayInasis.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }

        public void caliJunio()
        {
            Español = cmbJunEspañol.SelectedItem.ToString();
            Matematicas = cmbJunMatematicas.SelectedItem.ToString();
            Ingless = cmbJunIngles.SelectedItem.ToString();
            CienciasN = cmbJunCiencias.SelectedItem.ToString();
            LaEntidad = cmbJunEntidad.SelectedItem.ToString();
            FormacionCiv = cmbJunFormacion.SelectedItem.ToString();
            Artess = cmbJunArtes.SelectedItem.ToString();
            EducacionF = cmbJunEdFisica.SelectedItem.ToString();
            Edsocio = cmbJunSocio.SelectedItem.ToString();
            Inasistencias = cmbJunInasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        public void caliDiagnostico()
        {

            Español = cmbDiagEspañol.SelectedItem.ToString();
            Matematicas = cmbDiagMatematicas.SelectedItem.ToString();
            Ingless = cmbDiagIngles.SelectedItem.ToString();
            CienciasN = cmbDiagCiencias.SelectedItem.ToString();
            LaEntidad = cmbDiagEntidad.SelectedItem.ToString();
            FormacionCiv = cmbDiagFormacion.SelectedItem.ToString();
            Artess = cmbDiagArtes.SelectedItem.ToString();
            EducacionF = cmbDiagEdFisica.SelectedItem.ToString();
            Edsocio = cmbDiagSocio.SelectedItem.ToString();
            Inasistencias = cmbDiagInasis.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Ciencias Naturales' "; calificacion = Convert.ToDouble(CienciasN); buscarmateria(); insertarcali();
            materia = " 'La entidad donde vivo' "; calificacion = Convert.ToDouble(LaEntidad); buscarmateria(); insertarcali();
            materia = " 'Formación Cívica y Ética' "; calificacion = Convert.ToDouble(FormacionCiv); buscarmateria(); insertarcali();
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
                string diag_Ent = Calificaciones[4];
                string diag_FCE = Calificaciones[5];
                string diag_Artes = Calificaciones[6];
                string diag_Socio = Calificaciones[7];
                string diag_EdFis = Calificaciones[8];
                string diag_Inasis = Calificaciones[9];
                // SEPTIEMBRE
                string sept_Esp = Calificaciones[10];
                string sept_Mat = Calificaciones[11];
                string sept_Ing = Calificaciones[12];
                string sept_CiNat = Calificaciones[13];
                string sept_Ent = Calificaciones[14];
                string sept_FCE = Calificaciones[15];
                string sept_Artes = Calificaciones[16];
                string sept_Socio = Calificaciones[17];
                string sept_EdFis = Calificaciones[18];
                string sept_Inasis = Calificaciones[19];
                // OCTUBRE
                string oct_Esp = Calificaciones[20];
                string oct_Mat = Calificaciones[21];
                string oct_Ing = Calificaciones[22];
                string oct_CiNat = Calificaciones[23];
                string oct_Ent = Calificaciones[24];
                string oct_FCE = Calificaciones[25];
                string oct_Artes = Calificaciones[26];
                string oct_Socio = Calificaciones[27];
                string oct_EdFis = Calificaciones[28];
                string oct_Inasis = Calificaciones[29];
                // NOVIEMBRE
                string nov_Esp = Calificaciones[30];
                string nov_Mat = Calificaciones[31];
                string nov_Ing = Calificaciones[32];
                string nov_CiNat = Calificaciones[33];
                string nov_Ent = Calificaciones[34];
                string nov_FCE = Calificaciones[35];
                string nov_Artes = Calificaciones[36];
                string nov_Socio = Calificaciones[37];
                string nov_EdFis = Calificaciones[38];
                string nov_Inasis = Calificaciones[39];
                // DICIEMBRE
                string dic_Esp = Calificaciones[40];
                string dic_Mat = Calificaciones[41];
                string dic_Ing = Calificaciones[42];
                string dic_CiNat = Calificaciones[43];
                string dic_Ent = Calificaciones[44];
                string dic_FCE = Calificaciones[45];
                string dic_Artes = Calificaciones[46];
                string dic_Socio = Calificaciones[47];
                string dic_EdFis = Calificaciones[48];
                string dic_Inasis = Calificaciones[49];
                // ENERO
                string ene_Esp = Calificaciones[50];
                string ene_Mat = Calificaciones[51];
                string ene_Ing = Calificaciones[52];
                string ene_CiNat = Calificaciones[53];
                string ene_Ent = Calificaciones[54];
                string ene_FCE = Calificaciones[55];
                string ene_Artes = Calificaciones[56];
                string ene_Socio = Calificaciones[57];
                string ene_EdFis = Calificaciones[58];
                string ene_Inasis = Calificaciones[59];
                // FEBRERO
                string feb_Esp = Calificaciones[60];
                string feb_Mat = Calificaciones[61];
                string feb_Ing = Calificaciones[62];
                string feb_CiNat = Calificaciones[63];
                string feb_ent = Calificaciones[64];
                string feb_FCE = Calificaciones[65];
                string feb_Artes = Calificaciones[66];
                string feb_Socio = Calificaciones[67];
                string feb_EdFis = Calificaciones[68];
                string feb_Inasis = Calificaciones[69];
                // MARZO
                string mar_Esp = Calificaciones[70];
                string mar_Mat = Calificaciones[71];
                string mar_Ing = Calificaciones[72];
                string mar_CiNat = Calificaciones[73];
                string mar_ent = Calificaciones[74];
                string mar_FCE = Calificaciones[75];
                string mar_Artes = Calificaciones[76];
                string mar_Socio = Calificaciones[77];
                string mar_EdFis = Calificaciones[78];
                string mar_Inasis = Calificaciones[79];
                // ABRIL
                string abr_Esp = Calificaciones[80];
                string abr_Mat = Calificaciones[81];
                string abr_Ing = Calificaciones[82];
                string abr_CiNat = Calificaciones[83];
                string abr_ent = Calificaciones[84];
                string abr_FCE = Calificaciones[85];
                string abr_Artes = Calificaciones[86];
                string abr_Socio = Calificaciones[87];
                string abr_EdFis = Calificaciones[88];
                string abr_Inasis = Calificaciones[89];
                // MAYO
                string may_Esp = Calificaciones[90];
                string may_Mat = Calificaciones[91];
                string may_Ing = Calificaciones[92];
                string may_CiNat = Calificaciones[93];
                string may_ent = Calificaciones[94];
                string may_FCE = Calificaciones[95];
                string may_Artes = Calificaciones[96];
                string may_Socio = Calificaciones[97];
                string may_EdFis = Calificaciones[98];
                string may_Inasis = Calificaciones[99];
                // JUNIO
                string jun_Esp = Calificaciones[100];
                string jun_Mat = Calificaciones[101];
                string jun_Ing = Calificaciones[102];
                string jun_CiNat = Calificaciones[103];
                string jun_ent = Calificaciones[104];
                string jun_FCE = Calificaciones[105];
                string jun_Artes = Calificaciones[106];
                string jun_Socio = Calificaciones[107];
                string jun_EdFis = Calificaciones[108];
                string jun_Inasis = Calificaciones[109];


                // AGREGADO SEPTIEMBRE
                cmbSepEspañol.Text = sept_Esp;
                cmbSepMatematicas.Text = sept_Mat;
                cmbSepIngles.Text = sept_Ing;
                cmbSepCiencias.Text = sept_CiNat;
                cmbSepEntidad.Text = sept_Ent;
                cmbSepFormacion.Text = sept_FCE;
                cmbSepArtes.Text = sept_Artes;
                cmbSepSocio.Text = sept_Socio;
                cmbSepEdFisica.Text = sept_EdFis;
                cmbSepInasis.Text = sept_Inasis;
                // AGREGADO OCTUBRE
                cmbOctEspañol.Text = oct_Esp;
                cmbOctMatematicas.Text = oct_Mat;
                cmbOctIngles.Text = oct_Ing;
                cmbOctCiencias.Text = oct_CiNat;
                cmbOctEntidad.Text = oct_Ent;
                cmbOctFormacion.Text = oct_FCE;
                cmbOctArtes.Text = oct_Artes;
                cmbOctSocio.Text = oct_Socio;
                cmbOctEdFisica.Text = oct_EdFis;
                cmbOctInasis.Text = oct_Inasis;
                // AGREGADO NOVIEMBRE
                cmbNovEspañol.Text = nov_Esp;
                cmbNovMatematicas.Text = nov_Mat;
                cmbNovIngles.Text = nov_Ing;
                cmbNovCiencias.Text = nov_CiNat;
                cmbNovEntidad.Text = nov_Ent;
                cmbNovFormacion.Text = nov_FCE;
                cmbNovArtes.Text = nov_Artes;
                cmbNovSocio.Text = nov_Socio;
                cmbNovEdFisica.Text = nov_EdFis;
                cmbNovInasis.Text = nov_Inasis;
                // AGREGADO DICIEMBRE
                cmbDicEspañol.Text = dic_Esp;
                cmbDicMatematicas.Text = dic_Mat;
                cmbDicIngles.Text = dic_Ing;
                cmbDicCiencias.Text = dic_CiNat;
                cmbDicEntidad.Text = dic_Ent;
                cmbDicFormacion.Text = dic_FCE;
                cmbDicArtes.Text = dic_Artes;
                cmbDicSocio.Text = dic_Socio;
                cmbDicEdFisica.Text = dic_EdFis;
                cmbDicInasis.Text = dic_Inasis;
                // AGREGADO ENERO
                cmbEneEspañol.Text = ene_Esp;
                cmbEneMatematicas.Text = ene_Mat;
                cmbEneIngles.Text = ene_Ing;
                cmbEneCiencias.Text = ene_CiNat;
                cmbEneEntidad.Text = ene_Ent;
                cmbEneFormacion.Text = ene_FCE;
                cmbEneArtes.Text = ene_Artes;
                cmbEneSocio.Text = ene_Socio;
                cmbEneEdFisica.Text = ene_EdFis;
                cmbEneInasis.Text = ene_Inasis;
                // AGREGADO FEBRERO
                cmbFebEspañol.Text = feb_Esp;
                cmbFebMatematicas.Text = feb_Mat;
                cmbFebIngles.Text = feb_Ing;
                cmbFebCiencias.Text = feb_CiNat;
                cmbFebEntidad.Text = feb_ent;
                cmbFebFormacion.Text = feb_FCE;
                cmbFebArtes.Text = feb_Artes;
                cmbFebSocio.Text = feb_Socio;
                cmbFebEdFisica.Text = feb_EdFis;
                cmbFebInasis.Text = feb_Inasis;
                // AGREGADO MARZO
                cmbMarEspañol.Text = mar_Esp;
                cmbMarMatematicas.Text = mar_Mat;
                cmbMarIngles.Text = mar_Ing;
                cmbMarCiencias.Text = mar_CiNat;
                cmbMarEntidad.Text = mar_ent;
                cmbMarFormacion.Text = mar_FCE;
                cmbMarArtes.Text = mar_Artes;
                cmbMarSocio.Text = mar_Socio;
                cmbMarEdFisica.Text = mar_EdFis;
                cmbMarInasis.Text = mar_Inasis;
                // AGREGADO ABRIL
                cmbAbrEspañol.Text = abr_Esp;
                cmbAbrMatematicas.Text = abr_Mat;
                cmbAbrIngles.Text = abr_Ing;
                cmbAbrCiencias.Text = abr_CiNat;
                cmbAbrEntidad.Text = abr_ent;
                cmbAbrFormacion.Text = abr_FCE;
                cmbAbrArtes.Text = abr_Artes;
                cmbAbrSocio.Text = abr_Socio;
                cmbAbrEdFisica.Text = abr_EdFis;
                cmbAbrInasis.Text = abr_Inasis;
                // AGREGADO MAYO
                cmbMayEspañol.Text = may_Esp;
                cmbMayMatematicas.Text = may_Mat;
                cmbMayIngles.Text = may_Ing;
                cmbMayCiencias.Text = may_CiNat;
                cmbMayEntidad.Text = may_ent;
                cmbMayFormacion.Text = may_FCE;
                cmbMayArtes.Text = may_Artes;
                cmbMaySocio.Text = may_Socio;
                cmbMayEdFisica.Text = may_EdFis;
                cmbMayInasis.Text = may_Inasis;
                // AGREGADO JUNIO
                cmbJunEspañol.Text = jun_Esp;
                cmbJunMatematicas.Text = jun_Mat;
                cmbJunIngles.Text = jun_Ing;
                cmbJunCiencias.Text = jun_CiNat;
                cmbJunEntidad.Text = jun_ent;
                cmbJunFormacion.Text = jun_FCE;
                cmbJunArtes.Text = jun_Artes;
                cmbJunSocio.Text = jun_Socio;
                cmbJunEdFisica.Text = jun_EdFis;
                cmbJunInasis.Text = jun_Inasis;
                // AGREGADO DIAGNOSTICO
                cmbDiagEspañol.Text = diag_Esp;
                cmbDiagMatematicas.Text = diag_Mat;
                cmbDiagIngles.Text = diag_Ing;
                cmbDiagCiencias.Text = diag_CiNat;
                cmbDiagEntidad.Text = diag_Ent;
                cmbDiagFormacion.Text = diag_FCE;
                cmbDiagArtes.Text = diag_Artes;
                cmbDiagSocio.Text = diag_Socio;
                cmbDiagEdFisica.Text = diag_EdFis;
                cmbDiagInasis.Text = diag_Inasis;

                
                // BLOQUEO SEPTIEMBRE
                if (cmbSepEspañol.Text != "")
                {
                    cmbSepEspañol.Enabled = false;
                }
                if (cmbSepMatematicas.Text != "")
                {
                    cmbSepMatematicas.Enabled = false;
                }
                if (cmbSepCiencias.Text != "")
                {
                    cmbSepCiencias.Enabled = false;
                }
                if (cmbSepEntidad.Text != "")
                {
                    cmbSepEntidad.Enabled = false;
                }
                if (cmbSepFormacion.Text != "")
                {
                    cmbSepFormacion.Enabled = false;
                }
                if (cmbSepArtes.Text != "")
                {
                    cmbSepArtes.Enabled = false;
                }
                if (cmbSepSocio.Text != "")
                {
                    cmbSepSocio.Enabled = false;
                }
                if (cmbSepEdFisica.Text != "")
                {
                    cmbSepEdFisica.Enabled = false;
                }
                if (cmbSepIngles.Text != "")
                {
                    cmbSepIngles.Enabled = false;
                }
                if (cmbSepInasis.Text != "")
                {
                    cmbSepInasis.Enabled = false;
                }

                // BLOQUEO OCTUBRE
                if (cmbOctEspañol.Text != "")
                {
                    cmbOctEspañol.Enabled = false;
                }
                if (cmbOctMatematicas.Text != "")
                {
                    cmbOctMatematicas.Enabled = false;
                }
                if (cmbOctIngles.Text != "")
                {
                    cmbOctIngles.Enabled = false;
                }
                if (cmbOctCiencias.Text != "")
                {
                    cmbOctCiencias.Enabled = false;
                }
                if (cmbOctEntidad.Text != "")
                {
                    cmbOctEntidad.Enabled = false;
                }
                if (cmbOctFormacion.Text != "")
                {
                    cmbOctFormacion.Enabled = false;
                }
                if (cmbOctArtes.Text != "")
                {
                    cmbOctArtes.Enabled = false;
                }
                if (cmbOctSocio.Text != "")
                {
                    cmbOctSocio.Enabled = false;
                }
                if (cmbOctEdFisica.Text != "")
                {
                    cmbOctEdFisica.Enabled = false;
                }
                if (cmbOctInasis.Text != "")
                {
                    cmbOctInasis.Enabled = false;
                }

                // BLOQUEO NOVIEMBRE
                if (cmbNovEspañol.Text != "")
                {
                    cmbNovEspañol.Enabled = false;
                }
                if (cmbNovMatematicas.Text != "")
                {
                    cmbNovMatematicas.Enabled = false;
                }
                if (cmbNovIngles.Text != "")
                {
                    cmbNovIngles.Enabled = false;
                }
                if (cmbNovCiencias.Text != "")
                {
                    cmbNovCiencias.Enabled = false;
                }
                if (cmbNovEntidad.Text != "")
                {
                    cmbNovEntidad.Enabled = false;
                }
                if (cmbNovFormacion.Text != "")
                {
                    cmbNovFormacion.Enabled = false;
                }
                if (cmbNovArtes.Text != "")
                {
                    cmbNovArtes.Enabled = false;
                }
                if (cmbNovSocio.Text != "")
                {
                    cmbNovSocio.Enabled = false;
                }
                if (cmbNovEdFisica.Text != "")
                {
                    cmbNovEdFisica.Enabled = false;
                }
                if (cmbNovInasis.Text != "")
                {
                    cmbNovInasis.Enabled = false;
                }

                // BLOQUEO DICIEMBRE
                if (cmbDicEspañol.Text != "")
                {
                    cmbDicEspañol.Enabled = false;
                }
                if (cmbDicMatematicas.Text != "")
                {
                    cmbDicMatematicas.Enabled = false;
                }
                if (cmbDicIngles.Text != "")
                {
                    cmbDicIngles.Enabled = false;
                }
                if (cmbDicCiencias.Text != "")
                {
                    cmbDicCiencias.Enabled = false;
                }
                if (cmbDicEntidad.Text != "")
                {
                    cmbDicEntidad.Enabled = false;
                }
                if (cmbDicFormacion.Text != "")
                {
                    cmbDicFormacion.Enabled = false;
                }
                if (cmbDicArtes.Text != "")
                {
                    cmbDicArtes.Enabled = false;
                }
                if (cmbDicSocio.Text != "")
                {
                    cmbDicSocio.Enabled = false;
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
                if (cmbEneEspañol.Text != "")
                {
                    cmbEneEspañol.Enabled = false;
                }
                if (cmbEneMatematicas.Text != "")
                {
                    cmbEneMatematicas.Enabled = false;
                }
                if (cmbEneIngles.Text != "")
                {
                    cmbEneIngles.Enabled = false;
                }
                if (cmbEneCiencias.Text != "")
                {
                    cmbEneCiencias.Enabled = false;
                }
                if (cmbEneEntidad.Text != "")
                {
                    cmbEneEntidad.Enabled = false;
                }
                if (cmbEneFormacion.Text != "")
                {
                    cmbEneFormacion.Enabled = false;
                }
                if (cmbEneArtes.Text != "")
                {
                    cmbEneArtes.Enabled = false;
                }
                if (cmbEneSocio.Text != "")
                {
                    cmbEneSocio.Enabled = false;
                }
                if (cmbEneEdFisica.Text != "")
                {
                    cmbEneEdFisica.Enabled = false;
                }
                if (cmbEneInasis.Text != "")
                {
                    cmbEneInasis.Enabled = false;
                }

                // BLOQUEO FEBRERO
                if (cmbFebEspañol.Text != "")
                {
                    cmbFebEspañol.Enabled = false;
                }
                if (cmbFebMatematicas.Text != "")
                {
                    cmbFebMatematicas.Enabled = false;
                }
                if (cmbFebIngles.Text != "")
                {
                    cmbFebIngles.Enabled = false;
                }
                if (cmbFebCiencias.Text != "")
                {
                    cmbFebCiencias.Enabled = false;
                }
                if (cmbFebEntidad.Text != "")
                {
                    cmbFebEntidad.Enabled = false;
                }
                if (cmbFebFormacion.Text != "")
                {
                    cmbFebFormacion.Enabled = false;
                }
                if (cmbFebArtes.Text != "")
                {
                    cmbFebArtes.Enabled = false;
                }
                if (cmbFebSocio.Text != "")
                {
                    cmbFebSocio.Enabled = false;
                }
                if (cmbFebEdFisica.Text != "")
                {
                    cmbFebEdFisica.Enabled = false;
                }
                if (cmbFebInasis.Text != "")
                {
                    cmbFebInasis.Enabled = false;
                }

                // BLOQUEO MARZO
                if (cmbMarEspañol.Text != "")
                {
                    cmbMarEspañol.Enabled = false;
                }
                if (cmbMarMatematicas.Text != "")
                {
                    cmbMarMatematicas.Enabled = false;
                }
                if (cmbMarIngles.Text != "")
                {
                    cmbMarIngles.Enabled = false;
                }
                if (cmbMarCiencias.Text != "")
                {
                    cmbMarCiencias.Enabled = false;
                }
                if (cmbMarEntidad.Text != "")
                {
                    cmbMarEntidad.Enabled = false;
                }
                if (cmbMarFormacion.Text != "")
                {
                    cmbMarFormacion.Enabled = false;
                }
                if (cmbMarArtes.Text != "")
                {
                    cmbMarArtes.Enabled = false;
                }
                if (cmbMarSocio.Text != "")
                {
                    cmbMarSocio.Enabled = false;
                }
                if (cmbMarEdFisica.Text != "")
                {
                    cmbMarEdFisica.Enabled = false;
                }
                if (cmbMarInasis.Text != "")
                {
                    cmbMarInasis.Enabled = false;
                }

                // BLOQUEO ABRIL
                if (cmbAbrEspañol.Text != "")
                {
                    cmbAbrEspañol.Enabled = false;
                }
                if (cmbAbrMatematicas.Text != "")
                {
                    cmbAbrMatematicas.Enabled = false;
                }
                if (cmbAbrIngles.Text != "")
                {
                    cmbAbrIngles.Enabled = false;
                }
                if (cmbAbrCiencias.Text != "")
                {
                    cmbAbrCiencias.Enabled = false;
                }
                if (cmbAbrEntidad.Text != "")
                {
                    cmbAbrEntidad.Enabled = false;
                }
                if (cmbAbrFormacion.Text != "")
                {
                    cmbAbrFormacion.Enabled = false;
                }
                if (cmbAbrArtes.Text != "")
                {
                    cmbAbrArtes.Enabled = false;
                }
                if (cmbAbrSocio.Text != "")
                {
                    cmbAbrSocio.Enabled = false;
                }
                if (cmbAbrEdFisica.Text != "")
                {
                    cmbAbrEdFisica.Enabled = false;
                }
                if (cmbAbrInasis.Text != "")
                {
                    cmbAbrInasis.Enabled = false;
                }

                // BLOQUEO MAYO
                if (cmbMayEspañol.Text != "")
                {
                    cmbMayEspañol.Enabled = false;
                }
                if (cmbMayMatematicas.Text != "")
                {
                    cmbMayMatematicas.Enabled = false;
                }
                if (cmbMayIngles.Text != "")
                {
                    cmbMayIngles.Enabled = false;
                }
                if (cmbMayCiencias.Text != "")
                {
                    cmbMayCiencias.Enabled = false;
                }
                if (cmbMayEntidad.Text != "")
                {
                    cmbMayEntidad.Enabled = false;
                }
                if (cmbMayFormacion.Text != "")
                {
                    cmbMayFormacion.Enabled = false;
                }
                if (cmbMayArtes.Text != "")
                {
                    cmbMayArtes.Enabled = false;
                }
                if (cmbMaySocio.Text != "")
                {
                    cmbMaySocio.Enabled = false;
                }
                if (cmbMayEdFisica.Text != "")
                {
                    cmbMayEdFisica.Enabled = false;
                }
                if (cmbMayInasis.Text != "")
                {
                    cmbMayInasis.Enabled = false;
                }

                // BLOQUEO JUNIO
                if (cmbJunEspañol.Text != "")
                {
                    cmbJunEspañol.Enabled = false;
                }
                if (cmbJunMatematicas.Text != "")
                {
                    cmbJunMatematicas.Enabled = false;
                }
                if (cmbJunIngles.Text != "")
                {
                    cmbJunIngles.Enabled = false;
                }
                if (cmbJunCiencias.Text != "")
                {
                    cmbJunCiencias.Enabled = false;
                }
                if (cmbJunEntidad.Text != "")
                {
                    cmbJunEntidad.Enabled = false;
                }
                if (cmbJunFormacion.Text != "")
                {
                    cmbJunFormacion.Enabled = false;
                }
                if (cmbJunArtes.Text != "")
                {
                    cmbJunArtes.Enabled = false;
                }
                if (cmbJunSocio.Text != "")
                {
                    cmbJunSocio.Enabled = false;
                }
                if (cmbJunEdFisica.Text != "")
                {
                    cmbJunEdFisica.Enabled = false;
                }
                if (cmbJunInasis.Text != "")
                {
                    cmbJunInasis.Enabled = false;
                }

                // BLOQUEO DIAGNOSTICO
                if (cmbDiagEspañol.Text != "")
                {
                    cmbDiagEspañol.Enabled = false;
                }
                if (cmbDiagMatematicas.Text != "")
                {
                    cmbDiagMatematicas.Enabled = false;
                }
                if (cmbDiagIngles.Text != "")
                {
                    cmbDiagIngles.Enabled = false;
                }
                if (cmbDiagCiencias.Text != "")
                {
                    cmbDiagCiencias.Enabled = false;
                }
                if (cmbDiagEntidad.Text != "")
                {
                    cmbDiagEntidad.Enabled = false;
                }
                if (cmbDiagFormacion.Text != "")
                {
                    cmbDiagFormacion.Enabled = false;
                }
                if (cmbDiagArtes.Text != "")
                {
                    cmbDiagArtes.Enabled = false;
                }
                if (cmbDiagSocio.Text != "")
                {
                    cmbDiagSocio.Enabled = false;
                }
                if (cmbDiagEdFisica.Text != "")
                {
                    cmbDiagEdFisica.Enabled = false;
                }
                if (cmbDiagInasis.Text != "")
                {
                    cmbDiagInasis.Enabled = false;
                }


                //MessageBox.Show(sept_Esp.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ValidaCampos(GroupBox Grupo)
        {
            foreach (Control combo in Grupo.Controls)
            {
                if (combo is ComboBox)
                {
                    //ComboBox combo = new ComboBox();
                    //combo.Name = grupo.Name;
                    if (combo.Text == string.Empty)
                    {
                        MessageBox.Show("No se han Registrado todas las Calificaciones. Favor de llenar todos los campos.");
                        return true;
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

                    if (combo.Text != string.Empty)
                    {
                        double valor = Convert.ToDouble(combo.Text);
                        if (valor >= 5 && valor <= 5.9)
                        {
                            combo.ForeColor = Color.Red;

                        }

                        if (combo.Name == "cmbDiagIna" || combo.Name == "cmbSepIna" || combo.Name == "cmbOctIna" || combo.Name == "cmbNovIna" || combo.Name == "cmbDicIna" || combo.Name == "cmbEneroIna" || combo.Name == "cmbfebIna" || combo.Name == "cmbmarzIna" || combo.Name == "cmbAbrilIna" || combo.Name == "cmbMayoIna" || combo.Name == "cmbJunioIna")
                        {
                            combo.ForeColor = Color.Black;
                        }

                    }


                }

            }
        }

        private void Diagnostico_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox11);
        }

        private void Septiembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox1);
        }
                       
        private void Octubre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox2);
        }

        private void Noviembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox3);
        }

        private void Diciembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox4);
        }

        private void Enero_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox5);
        }

        private void Febrero_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox6);
        }

        private void Marzo_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox7);
        }

        private void Abril_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox9);
        }

        private void Mayo_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox8);
        }

        private void Junio_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox10);
        }

    }



}
