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
//CAMBIOS EVERYWHERE!!!
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
            cambiacolor(BoxGenero);
        }

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

        private void btnPrincipal_Click_1(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnIrBoletas_Click(object sender, EventArgs e)
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

            string[] CalifSept = new string[11];
            string PromSept = " ";
            string PromSeptGeneral = " ";

            if (myreader1.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader1.Read())//Agrega calificaciones
                {
                    double[] CalifSeptemp = new double[11];
                    CalifSeptemp[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                    CalifSept[L] = CalifSeptemp[L].ToString("0.#");
                    L++;
                }

                double PromSeptTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromSeptTemp = PromSeptTemp + Convert.ToDouble(CalifSept[i]);
                }

                PromSeptTemp = PromSeptTemp / 9;
                PromSept = PromSeptTemp.ToString("0.#"); //Promedio de septiembre

                double PromSeptGen = 0;
                
                PromSeptGen = Convert.ToDouble(PromSept) + Convert.ToDouble(CalifSept[9]);
                PromSeptGen = PromSeptGen / 2;
                PromSeptGeneral = PromSeptGen.ToString("0.#"); //Promedio general de semptiembre
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifSept[i] = " ";
                }
            }
            conn1.Close();

            //Octubre-----------------------------------------------------------------
            string CalifOct = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Octubre'";

            MySqlConnection conn2;
            MySqlCommand com2;

            conn2 = new MySqlConnection(conexion);
            conn2.Open();

            com2 = new MySqlCommand(CalifOct, conn2);

            MySqlDataReader myreader2 = com2.ExecuteReader();

            string[] CalifOctu = new string[11];
            string PromOct = " ";
            string PromOctuGeneral = " ";

            if (myreader2.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader2.Read())//Agrega calificaciones
                {
                    double[] CalifOctutemp = new double[11];
                    CalifOctutemp[L] = Convert.ToDouble(myreader2["CalificacionMen"]);
                    CalifOctu[L] = CalifOctutemp[L].ToString("0.#");
                    L++;
                }

                double PromOctTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromOctTemp = PromOctTemp + Convert.ToDouble(CalifOctu[i]);
                }

                PromOctTemp = PromOctTemp / 9;
                PromOct = PromOctTemp.ToString("0.#"); //Promedio de octubre

                double PromOctGen = 0;

                PromOctGen = Convert.ToDouble(PromOct) + Convert.ToDouble(CalifOctu[9]);
                PromOctGen = PromOctGen / 2;
                PromOctuGeneral = PromOctGen.ToString("0.#"); //Promedio general de semptiembre
            }
            else
            {

                for (int i = 0; i < 11; i++)
                {
                    CalifOctu[i] = " ";
                }
            }
            conn2.Close();
            //Noviembre-----------------------------------------------------------------
            string CalifNov = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Noviembre'";

            MySqlConnection conn3;
            MySqlCommand com3;

            conn3 = new MySqlConnection(conexion);
            conn3.Open();

            com3 = new MySqlCommand(CalifNov, conn3);

            MySqlDataReader myreader3 = com3.ExecuteReader();

            string[] CalifNovi = new string[11];
            string PromNov = " ";
            string PromTriSen1 = " ";
            string PromTriEsp1 = " ";
            string PromTriMat1 = " ";
            string PromTriIng1 = " ";
            string PromTricn1 = " ";
            string PromTriGeo1 = " ";
            string PromTriHis1 = " ";
            string PromTriCiv1 = " ";
            string PromTriArt1 = " ";
            string PromTriFis1 = " ";
            string PromTriSoc1 = " ";
            string SumaInasisTri1 = " ";
            string NivelPromSen = " ";
            string Nivel1 = " ";
            string NivelMat1 = " ";
            string NivelIng1 = " ";
            string Nivelcn1 = " ";
            string NivelGeo1 = " ";
            string NivelHis1 = " ";
            string NivelCiv1 = " ";
            string NivelArt1 = " ";
            string NivelFis1 = " ";
            string NivelSoc1 = " ";
            string PromNoviGeneral = " ";
            string PromTriGen1 = " ";
            string NivelGen1 = " ";

            if (myreader3.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader3.Read())//Agrega calificaciones
                {
                    double[] CalifNovitemp = new double[11];
                    CalifNovitemp[L] = Convert.ToDouble(myreader3["CalificacionMen"]);
                    CalifNovi[L] = CalifNovitemp[L].ToString("0.#");
                    L++;
                }

                double PromNovTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromNovTemp = PromNovTemp + Convert.ToDouble(CalifNovi[i]);
                }

                PromNovTemp = PromNovTemp / 9;
                PromNov = PromNovTemp.ToString("0.#");//Promedio de noviembre

                double PromNovGen = 0;

                PromNovGen = Convert.ToDouble(PromNov) + Convert.ToDouble(CalifNovi[9]);
                PromNovGen = PromNovGen / 2;
                PromNoviGeneral = PromNovGen.ToString("0.#"); //Promedio general de nociembre

                //------------------------------------Promedio de materias sencillas primer trimestre-------------------------------------------------
                double PromSen31 = 0;

                PromSen31 = Convert.ToDouble(PromSept) + Convert.ToDouble(PromOct) + Convert.ToDouble(PromNov);
                PromSen31 = PromSen31 / 3;
                PromTriSen1 = PromSen31.ToString("0.#");//Promedio del primer trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                {
                    NivelPromSen = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                    {
                        NivelPromSen = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                        {
                            NivelPromSen = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) == 10)
                            {
                                NivelPromSen = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelPromSen = " ";
                            }
                        }
                    }
                }

                //------------------------------------Promedio General primer trimestre-------------------------------------------------
                double PromGen31 = 0;

                PromGen31 = Convert.ToDouble(PromSeptGeneral) + Convert.ToDouble(PromOctuGeneral) + Convert.ToDouble(PromNoviGeneral);
                PromGen31 = PromGen31 / 3;
                PromTriGen1 = PromGen31.ToString("0.#");//Promedio del primer trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriGen1) >= 5 && Convert.ToDouble(PromTriGen1) < 6)
                {
                    NivelGen1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGen1) >= 6 && Convert.ToDouble(PromTriGen1) < 8)
                    {
                        NivelGen1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen1) >= 8 && Convert.ToDouble(PromTriGen1) < 10)
                        {
                            NivelGen1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen1) == 10)
                            {
                                NivelGen1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGen1 = " ";
                            }
                        }
                    }
                }

                //------------------------------------Español-------------------------------------------------
                double PromEsp31 = 0;

                PromEsp31 = Convert.ToDouble(CalifSept[0]) + Convert.ToDouble(CalifOctu[0]) + Convert.ToDouble(CalifNovi[0]);
                PromEsp31 = PromEsp31 / 3;
                PromTriEsp1 = PromEsp31.ToString("0.#");//Promedio del primer trimestre de español

                //Asignacion del nivel del primer trimestre de español
                if (Convert.ToDouble(PromTriEsp1) >= 5 && Convert.ToDouble(PromTriEsp1) < 6)
                {
                    Nivel1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriEsp1) >= 6 && Convert.ToDouble(PromTriEsp1) < 8)
                    {
                        Nivel1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp1) >= 8 && Convert.ToDouble(PromTriEsp1) < 10)
                        {
                            Nivel1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp1) == 10)
                            {
                                Nivel1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivel1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Matematicas--------------------------------------------------------
                double PromMat31 = 0;

                PromMat31 = Convert.ToDouble(CalifSept[1]) + Convert.ToDouble(CalifOctu[1]) + Convert.ToDouble(CalifNovi[1]);
                PromMat31 = PromMat31 / 3;
                PromTriMat1 = PromMat31.ToString("0.#");//Promedio del primer trimestre de matematicas

                //Asignacion del nivel del primer trimestre de matematicas
                if (Convert.ToDouble(PromTriMat1) >= 5 && Convert.ToDouble(PromTriMat1) < 6)
                {
                    NivelMat1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriMat1) >= 6 && Convert.ToDouble(PromTriMat1) < 8)
                    {
                        NivelMat1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat1) >= 8 && Convert.ToDouble(PromTriMat1) < 10)
                        {
                            NivelMat1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat1) == 10)
                            {
                                NivelMat1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelMat1 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ingles-------------------------------------------------
                double PromIng31 = 0;

                PromIng31 = Convert.ToDouble(CalifSept[2]) + Convert.ToDouble(CalifOctu[2]) + Convert.ToDouble(CalifNovi[2]);
                PromIng31 = PromIng31 / 3;
                PromTriIng1 = PromIng31.ToString("0.#");//Promedio del primer trimestre de Ingles

                //Asignacion del nivel del primer trimestre de Ingles
                if (Convert.ToDouble(PromTriIng1) >= 5 && Convert.ToDouble(PromTriIng1) < 6)
                {
                    NivelIng1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriIng1) >= 6 && Convert.ToDouble(PromTriIng1) < 8)
                    {
                        NivelIng1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng1) >= 8 && Convert.ToDouble(PromTriIng1) < 10)
                        {
                            NivelIng1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng1) == 10)
                            {
                                NivelIng1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelIng1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                double Promcn31 = 0;

                Promcn31 = Convert.ToDouble(CalifSept[3]) + Convert.ToDouble(CalifOctu[3]) + Convert.ToDouble(CalifNovi[3]);
                Promcn31 = Promcn31 / 3;
                PromTricn1 = Promcn31.ToString("0.#");//Promedio del primer trimestre de Ciencias

                //Asignacion del nivel del primer trimestre de Ciencias
                if (Convert.ToDouble(PromTricn1) >= 5 && Convert.ToDouble(PromTricn1) < 6)
                {
                    Nivelcn1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTricn1) >= 6 && Convert.ToDouble(PromTricn1) < 8)
                    {
                        Nivelcn1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn1) >= 8 && Convert.ToDouble(PromTricn1) < 10)
                        {
                            Nivelcn1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn1) == 10)
                            {
                                Nivelcn1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivelcn1 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Geografia-------------------------------------------------
                double PromGeo31 = 0;

                PromGeo31 = Convert.ToDouble(CalifSept[4]) + Convert.ToDouble(CalifOctu[4]) + Convert.ToDouble(CalifNovi[4]);
                PromGeo31 = PromGeo31 / 3;
                PromTriGeo1 = PromGeo31.ToString("0.#");//Promedio del primer trimestre de Geografia

                //Asignacion del nivel del primer trimestre de Geografia
                if (Convert.ToDouble(PromTriGeo1) >= 5 && Convert.ToDouble(PromTriGeo1) < 6)
                {
                    NivelGeo1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGeo1) >= 6 && Convert.ToDouble(PromTriGeo1) < 8)
                    {
                        NivelGeo1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo1) >= 8 && Convert.ToDouble(PromTriGeo1) < 10)
                        {
                            NivelGeo1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo1) == 10)
                            {
                                NivelGeo1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGeo1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Historia--------------------------------------------------------
                double PromHis31 = 0;

                PromHis31 = Convert.ToDouble(CalifSept[5]) + Convert.ToDouble(CalifOctu[5]) + Convert.ToDouble(CalifNovi[5]);
                PromHis31 = PromHis31 / 3;
                PromTriHis1 = PromHis31.ToString("0.#");//Promedio del primer trimestre de Historia

                //Asignacion del nivel del primer trimestre de Historia
                if (Convert.ToDouble(PromTriHis1) >= 5 && Convert.ToDouble(PromTriHis1) < 6)
                {
                    NivelHis1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriHis1) >= 6 && Convert.ToDouble(PromTriHis1) < 8)
                    {
                        NivelHis1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis1) >= 8 && Convert.ToDouble(PromTriHis1) < 10)
                        {
                            NivelHis1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis1) == 10)
                            {
                                NivelHis1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelHis1 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Formacion Civica y etica-------------------------------------------------
                double PromCiv31 = 0;

                PromCiv31 = Convert.ToDouble(CalifSept[6]) + Convert.ToDouble(CalifOctu[6]) + Convert.ToDouble(CalifNovi[6]);
                PromCiv31 = PromCiv31 / 3;
                PromTriCiv1 = PromCiv31.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                if (Convert.ToDouble(PromTriCiv1) >= 5 && Convert.ToDouble(PromTriCiv1) < 6)
                {
                    NivelCiv1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCiv1) >= 6 && Convert.ToDouble(PromTriCiv1) < 8)
                    {
                        NivelCiv1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv1) >= 8 && Convert.ToDouble(PromTriCiv1) < 10)
                        {
                            NivelCiv1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv1) == 10)
                            {
                                NivelCiv1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCiv1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt31 = 0;

                PromArt31 = Convert.ToDouble(CalifSept[7]) + Convert.ToDouble(CalifOctu[7]) + Convert.ToDouble(CalifNovi[7]);
                PromArt31 = PromArt31 / 3;
                PromTriArt1 = PromArt31.ToString("0.#");//Promedio del primer trimestre de Artes

                //Asignacion del nivel del primer trimestre de Artes
                if (Convert.ToDouble(PromTriArt1) >= 5 && Convert.ToDouble(PromTriArt1) < 6)
                {
                    NivelArt1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriArt1) >= 6 && Convert.ToDouble(PromTriArt1) < 8)
                    {
                        NivelArt1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt1) >= 8 && Convert.ToDouble(PromTriArt1) < 10)
                        {
                            NivelArt1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt1) == 10)
                            {
                                NivelArt1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelArt1 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ed. Fisica-------------------------------------------------
                double PromFis31 = 0;

                PromFis31 = Convert.ToDouble(CalifSept[8]) + Convert.ToDouble(CalifOctu[8]) + Convert.ToDouble(CalifNovi[8]);
                PromFis31 = PromFis31 / 3;
                PromTriFis1 = PromFis31.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                //Asignacion del nivel del primer trimestre de Ed. Fisica
                if (Convert.ToDouble(PromTriFis1) >= 5 && Convert.ToDouble(PromTriFis1) < 6)
                {
                    NivelFis1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriFis1) >= 6 && Convert.ToDouble(PromTriFis1) < 8)
                    {
                        NivelFis1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis1) >= 8 && Convert.ToDouble(PromTriFis1) < 10)
                        {
                            NivelFis1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis1) == 10)
                            {
                                NivelFis1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelFis1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                double PromSoc31 = 0;

                PromSoc31 = Convert.ToDouble(CalifSept[9]) + Convert.ToDouble(CalifOctu[9]) + Convert.ToDouble(CalifNovi[9]);
                PromSoc31 = PromSoc31 / 3;
                PromTriSoc1 = PromSoc31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                if (Convert.ToDouble(PromTriSoc1) >= 5 && Convert.ToDouble(PromTriSoc1) < 6)
                {
                    NivelSoc1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSoc1) >= 6 && Convert.ToDouble(PromTriSoc1) < 8)
                    {
                        NivelSoc1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc1) >= 8 && Convert.ToDouble(PromTriSoc1) < 10)
                        {
                            NivelSoc1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc1) == 10)
                            {
                                NivelSoc1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelSoc1 = " ";
                            }
                        }
                    }
                }

                //------------------------------------Inasistencias----------------------------------------------------
                double SumaInasis31 = 0;

                SumaInasis31 = Convert.ToDouble(CalifSept[10]) + Convert.ToDouble(CalifOctu[10]) + Convert.ToDouble(CalifNovi[10]);
                SumaInasisTri1 = SumaInasis31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
            }
            else
            {

                for (int i = 0; i < 11; i++)
                {
                    CalifNovi[i] = " ";
                }
            }
            conn3.Close();

            //Diciembre-----------------------------------------------------------------
            string CalifDic = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Diciembre'";

            MySqlConnection conn4;
            MySqlCommand com4;

            conn4 = new MySqlConnection(conexion);
            conn4.Open();

            com4 = new MySqlCommand(CalifDic, conn4);

            MySqlDataReader myreader4 = com4.ExecuteReader();

            string[] CalifDici = new string[11];
            string PromDic = " ";
            string PromDiciGeneral = " ";

            if (myreader4.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader4.Read())//Agrega calificaciones
                {
                    double[] CalifDicitemp = new double[11];
                    CalifDicitemp[L] = Convert.ToDouble(myreader4["CalificacionMen"]);
                    CalifDici[L] = CalifDicitemp[L].ToString("0.#");
                    L++;
                }

                double PromDicTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromDicTemp = PromDicTemp + Convert.ToDouble(CalifDici[i]);
                }

                PromDicTemp = PromDicTemp / 9;
                PromDic = PromDicTemp.ToString("0.#");//Promedio de diciembre

                double PromDiciGen = 0;

                PromDiciGen = Convert.ToDouble(PromDic) + Convert.ToDouble(CalifDici[9]);
                PromDiciGen = PromDiciGen / 2;
                PromDiciGeneral = PromDiciGen.ToString("0.#"); //Promedio general de diciembre
            }
            else
            {

                for (int i = 0; i < 11; i++)
                {
                    CalifDici[i] = " ";
                }
            }
            conn4.Close();
            //Enero-----------------------------------------------------------------
            string CalifEne = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Enero'";

            MySqlConnection conn5;
            MySqlCommand com5;

            conn5 = new MySqlConnection(conexion);
            conn5.Open();

            com5 = new MySqlCommand(CalifEne, conn5);

            MySqlDataReader myreader5 = com5.ExecuteReader();

            string[] CalifEner = new string[11];
            string PromEne = " ";
            string PromEnerGeneral = " ";

            if (myreader5.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader5.Read())//Agrega calificaciones
                {
                    double[] CalifEnertemp = new double[11];
                    CalifEnertemp[L] = Convert.ToDouble(myreader5["CalificacionMen"]);
                    CalifEner[L] = CalifEnertemp[L].ToString("0.#");
                    L++;
                }

                double PromEneTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromEneTemp = PromEneTemp + Convert.ToDouble(CalifEner[i]);
                }

                PromEneTemp = PromEneTemp / 9;
                PromEne = PromEneTemp.ToString("0.#");//Promedio de enero

                double PromEnerGen = 0;

                PromEnerGen = Convert.ToDouble(PromEne) + Convert.ToDouble(CalifEner[9]);
                PromEnerGen = PromEnerGen / 2;
                PromEnerGeneral = PromEnerGen.ToString("0.#"); //Promedio general de enero
            }
            else
            {

                for (int i = 0; i < 11; i++)
                {
                    CalifEner[i] = " ";
                }
            }
            conn5.Close();

            //Febrero-----------------------------------------------------------------
            string CalifFeb = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Febrero'";

            MySqlConnection conn6;
            MySqlCommand com6;

            conn6 = new MySqlConnection(conexion);
            conn6.Open();

            com6 = new MySqlCommand(CalifFeb, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();

            string[] CalifFebr = new string[11];
            string PromFeb = " ";
            string PromTriEsp2 = " ";
            string PromTriMat2 = " ";
            string PromTriIng2 = " ";
            string PromTricn2 = " ";
            string PromTriGeo2 = " ";
            string PromTriHis2 = " ";
            string PromTriCiv2 = " ";
            string PromTriArt2 = " ";
            string PromTriFis2 = " ";
            string PromTriSoc2 = " ";
            string PromTriSen2 = " ";
            string SumaInasisTri2 = " ";
            string Nivel2 = " ";
            string NivelMat2 = " ";
            string NivelIng2 = " ";
            string Nivelcn2 = " ";
            string NivelGeo2 = " ";
            string NivelHis2 = " ";
            string NivelCiv2 = " ";
            string NivelArt2 = " ";
            string NivelFis2 = " ";
            string NivelSoc2 = " ";
            string NivelPromSen2 = " ";
            string PromFebrGeneral = " ";
            string PromTriGen2 = " ";
            string NivelGen2 = " ";

            if (myreader6.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader6.Read())//Agrega calificaciones
                {
                    double[] CalifFebrtemp = new double[11];
                    CalifFebrtemp[L] = Convert.ToDouble(myreader6["CalificacionMen"]);
                    CalifFebr[L] = CalifFebrtemp[L].ToString("0.#");
                    L++;
                }

                double PromFebTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromFebTemp = PromFebTemp + Convert.ToDouble(CalifFebr[i]);
                }

                PromFebTemp = PromFebTemp / 9;
                PromFeb = PromFebTemp.ToString("0.#");//Promedio de febrero

                double PromFebrGen = 0;

                PromFebrGen = Convert.ToDouble(PromFeb) + Convert.ToDouble(CalifFebr[9]);
                PromFebrGen = PromFebrGen / 2;
                PromFebrGeneral = PromFebrGen.ToString("0.#"); //Promedio general de febrero

                //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                double PromSen32 = 0;

                PromSen32 = Convert.ToDouble(PromEne) + Convert.ToDouble(PromDic) + Convert.ToDouble(PromFeb);
                PromSen32 = PromSen32 / 3;
                PromTriSen2 = PromSen32.ToString("0.#");//Promedio del segundo trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                {
                    NivelPromSen2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                    {
                        NivelPromSen2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                        {
                            NivelPromSen2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) == 10)
                            {
                                NivelPromSen2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelPromSen2 = " ";
                            }
                        }
                    }
                }

                //------------------------------------Promedio General segundo trimestre-------------------------------------------------
                double PromGen32 = 0;

                PromGen32 = Convert.ToDouble(PromDiciGeneral) + Convert.ToDouble(PromEnerGeneral) + Convert.ToDouble(PromFebrGeneral);
                PromGen32 = PromGen32 / 3;
                PromTriGen2 = PromGen32.ToString("0.#");//Promedio del primer trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriGen2) >= 5 && Convert.ToDouble(PromTriGen2) < 6)
                {
                    NivelGen2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGen2) >= 6 && Convert.ToDouble(PromTriGen2) < 8)
                    {
                        NivelGen2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen2) >= 8 && Convert.ToDouble(PromTriGen2) < 10)
                        {
                            NivelGen2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen2) == 10)
                            {
                                NivelGen2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGen2 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Español-------------------------------------------------
                double PromEsp32 = 0;

                PromEsp32 = Convert.ToDouble(CalifDici[0]) + Convert.ToDouble(CalifEner[0]) + Convert.ToDouble(CalifFebr[0]);
                PromEsp32 = PromEsp32 / 3;
                PromTriEsp2 = PromEsp32.ToString("0.#");//Promedio del primer trimestre de español

                //Asignacion del nivel del primer trimestre de español
                if (Convert.ToDouble(PromTriEsp2) >= 5 && Convert.ToDouble(PromTriEsp2) < 6)
                {
                    Nivel2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriEsp2) >= 6 && Convert.ToDouble(PromTriEsp2) < 8)
                    {
                        Nivel2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp2) >= 8 && Convert.ToDouble(PromTriEsp2) < 10)
                        {
                            Nivel2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp2) == 10)
                            {
                                Nivel2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivel2 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Matematicas--------------------------------------------------------
                double PromMat32 = 0;

                PromMat32 = Convert.ToDouble(CalifDici[1]) + Convert.ToDouble(CalifEner[1]) + Convert.ToDouble(CalifFebr[1]);
                PromMat32 = PromMat32 / 3;
                PromTriMat2 = PromMat32.ToString("0.#");//Promedio del primer trimestre de matematicas

                //Asignacion del nivel del primer trimestre de matematicas
                if (Convert.ToDouble(PromTriMat2) >= 5 && Convert.ToDouble(PromTriMat2) < 6)
                {
                    NivelMat2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriMat2) >= 6 && Convert.ToDouble(PromTriMat2) < 8)
                    {
                        NivelMat2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat2) >= 8 && Convert.ToDouble(PromTriMat2) < 10)
                        {
                            NivelMat2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat2) == 10)
                            {
                                NivelMat2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelMat2 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ingles-------------------------------------------------
                double PromIng32 = 0;

                PromIng32 = Convert.ToDouble(CalifDici[2]) + Convert.ToDouble(CalifEner[2]) + Convert.ToDouble(CalifFebr[2]);
                PromIng32 = PromIng32 / 3;
                PromTriIng2 = PromIng32.ToString("0.#");//Promedio del primer trimestre de Ingles

                //Asignacion del nivel del primer trimestre de Ingles
                if (Convert.ToDouble(PromTriIng2) >= 5 && Convert.ToDouble(PromTriIng2) < 6)
                {
                    NivelIng2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriIng2) >= 6 && Convert.ToDouble(PromTriIng2) < 8)
                    {
                        NivelIng2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng2) >= 8 && Convert.ToDouble(PromTriIng2) < 10)
                        {
                            NivelIng2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng2) == 10)
                            {
                                NivelIng2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelIng2 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                double Promcn32 = 0;

                Promcn32 = Convert.ToDouble(CalifDici[3]) + Convert.ToDouble(CalifEner[3]) + Convert.ToDouble(CalifFebr[3]);
                Promcn32 = Promcn32 / 3;
                PromTricn2 = Promcn32.ToString("0.#");//Promedio del primer trimestre de Ciencias

                //Asignacion del nivel del primer trimestre de Ciencias
                if (Convert.ToDouble(PromTricn2) >= 5 && Convert.ToDouble(PromTricn2) < 6)
                {
                    Nivelcn2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTricn2) >= 6 && Convert.ToDouble(PromTricn2) < 8)
                    {
                        Nivelcn2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn2) >= 8 && Convert.ToDouble(PromTricn2) < 10)
                        {
                            Nivelcn2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn2) == 10)
                            {
                                Nivelcn2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivelcn2 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Geografia-------------------------------------------------
                double PromGeo32 = 0;

                PromGeo32 = Convert.ToDouble(CalifDici[4]) + Convert.ToDouble(CalifEner[4]) + Convert.ToDouble(CalifFebr[4]);
                PromGeo32 = PromGeo32 / 3;
                PromTriGeo2 = PromGeo32.ToString("0.#");//Promedio del primer trimestre de Geografia

                //Asignacion del nivel del primer trimestre de Geografia
                if (Convert.ToDouble(PromTriGeo2) >= 5 && Convert.ToDouble(PromTriGeo2) < 6)
                {
                    NivelGeo2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGeo2) >= 6 && Convert.ToDouble(PromTriGeo2) < 8)
                    {
                        NivelGeo2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo2) >= 8 && Convert.ToDouble(PromTriGeo2) < 10)
                        {
                            NivelGeo2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo2) == 10)
                            {
                                NivelGeo2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGeo2 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Historia--------------------------------------------------------
                double PromHis32 = 0;

                PromHis32 = Convert.ToDouble(CalifDici[5]) + Convert.ToDouble(CalifEner[5]) + Convert.ToDouble(CalifFebr[5]);
                PromHis32 = PromHis32 / 3;
                PromTriHis2 = PromHis32.ToString("0.#");//Promedio del primer trimestre de Historia

                //Asignacion del nivel del primer trimestre de Historia
                if (Convert.ToDouble(PromTriHis2) >= 5 && Convert.ToDouble(PromTriHis2) < 6)
                {
                    NivelHis2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriHis2) >= 6 && Convert.ToDouble(PromTriHis2) < 8)
                    {
                        NivelHis2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis2) >= 8 && Convert.ToDouble(PromTriHis2) < 10)
                        {
                            NivelHis2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis2) == 10)
                            {
                                NivelHis2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelHis2 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Formacion Civica y etica-------------------------------------------------
                double PromCiv32 = 0;

                PromCiv32 = Convert.ToDouble(CalifDici[6]) + Convert.ToDouble(CalifEner[6]) + Convert.ToDouble(CalifFebr[6]);
                PromCiv32 = PromCiv32 / 3;
                PromTriCiv2 = PromCiv32.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                if (Convert.ToDouble(PromTriCiv2) >= 5 && Convert.ToDouble(PromTriCiv2) < 6)
                {
                    NivelCiv2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCiv2) >= 6 && Convert.ToDouble(PromTriCiv2) < 8)
                    {
                        NivelCiv2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv2) >= 8 && Convert.ToDouble(PromTriCiv2) < 10)
                        {
                            NivelCiv2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv2) == 10)
                            {
                                NivelCiv2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCiv2 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt32 = 0;

                PromArt32 = Convert.ToDouble(CalifDici[7]) + Convert.ToDouble(CalifEner[7]) + Convert.ToDouble(CalifFebr[7]);
                PromArt32 = PromArt32 / 3;
                PromTriArt2 = PromArt32.ToString("0.#");//Promedio del primer trimestre de Artes

                //Asignacion del nivel del primer trimestre de Artes
                if (Convert.ToDouble(PromTriArt2) >= 5 && Convert.ToDouble(PromTriArt2) < 6)
                {
                    NivelArt2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriArt2) >= 6 && Convert.ToDouble(PromTriArt2) < 8)
                    {
                        NivelArt2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt2) >= 8 && Convert.ToDouble(PromTriArt2) < 10)
                        {
                            NivelArt2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt2) == 10)
                            {
                                NivelArt2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelArt2 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ed. Fisica-------------------------------------------------
                double PromFis32 = 0;

                PromFis32 = Convert.ToDouble(CalifDici[8]) + Convert.ToDouble(CalifEner[8]) + Convert.ToDouble(CalifFebr[8]);
                PromFis32 = PromFis32 / 3;
                PromTriFis2 = PromFis32.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                //Asignacion del nivel del primer trimestre de Ed. Fisica
                if (Convert.ToDouble(PromTriFis2) >= 5 && Convert.ToDouble(PromTriFis2) < 6)
                {
                    NivelFis2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriFis2) >= 6 && Convert.ToDouble(PromTriFis2) < 8)
                    {
                        NivelFis2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis2) >= 8 && Convert.ToDouble(PromTriFis2) < 10)
                        {
                            NivelFis2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis2) == 10)
                            {
                                NivelFis2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelFis2 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                double PromSoc32 = 0;

                PromSoc32 = Convert.ToDouble(CalifDici[9]) + Convert.ToDouble(CalifEner[9]) + Convert.ToDouble(CalifFebr[9]);
                PromSoc32 = PromSoc32 / 3;
                PromTriSoc2 = PromSoc32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                if (Convert.ToDouble(PromTriSoc2) >= 5 && Convert.ToDouble(PromTriSoc2) < 6)
                {
                    NivelSoc2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSoc2) >= 6 && Convert.ToDouble(PromTriSoc2) < 8)
                    {
                        NivelSoc2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc2) >= 8 && Convert.ToDouble(PromTriSoc2) < 10)
                        {
                            NivelSoc2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc2) == 10)
                            {
                                NivelSoc2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelSoc2 = " ";
                            }
                        }
                    }
                }

                //-----------------------------Inasistencia-----------------------------------------------------
                double SumaInasis32 = 0;

                SumaInasis32 = Convert.ToDouble(CalifDici[10]) + Convert.ToDouble(CalifEner[10]) + Convert.ToDouble(CalifFebr[10]);
                SumaInasisTri2 = SumaInasis32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifFebr[i] = " ";
                }
            }
            conn6.Close();
            //Marzo-----------------------------------------------------------------
            string CalifMar = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Marzo'";

            MySqlConnection conn7;
            MySqlCommand com7;

            conn7 = new MySqlConnection(conexion);
            conn7.Open();

            com7 = new MySqlCommand(CalifMar, conn7);

            MySqlDataReader myreader7 = com7.ExecuteReader();

            string[] CalifMarz = new string[11];
            string PromMar = " ";
            string PromMarzGeneral = " ";

            if (myreader7.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader7.Read())//Agrega calificaciones
                {
                    double[] CalifMarztemp = new double[11];
                    CalifMarztemp[L] = Convert.ToDouble(myreader7["CalificacionMen"]);
                    CalifMarz[L] = CalifMarztemp[L].ToString("0.#");
                    L++;
                }

                double PromMarTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromMarTemp = PromMarTemp + Convert.ToDouble(CalifMarz[i]);
                }

                PromMarTemp = PromMarTemp / 9;
                PromMar = PromMarTemp.ToString("0.#");//Promedio de  marzo

                double PromMarzGen = 0;

                PromMarzGen = Convert.ToDouble(PromMar) + Convert.ToDouble(CalifMarz[9]);
                PromMarzGen = PromMarzGen / 2;
                PromMarzGeneral = PromMarzGen.ToString("0.#"); //Promedio general de marzo

            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifMarz[i] = " ";
                }
            }
            conn7.Close();
            //Abril-----------------------------------------------------------------
            string CalifAbr = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Abril'";

            MySqlConnection conn8;
            MySqlCommand com8;

            conn8 = new MySqlConnection(conexion);
            conn8.Open();

            com8 = new MySqlCommand(CalifAbr, conn8);

            MySqlDataReader myreader8 = com8.ExecuteReader();

            string[] CalifAbri = new string[11];
            string PromAbr = " ";
            string PromAbriGeneral = " ";

            if (myreader8.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader8.Read())//Agrega calificaciones
                {
                    double[] CalifAbritemp = new double[11];
                    CalifAbritemp[L] = Convert.ToDouble(myreader8["CalificacionMen"]);
                    CalifAbri[L] = CalifAbritemp[L].ToString("0.#");
                    L++;
                }

                double PromAbrTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromAbrTemp = PromAbrTemp + Convert.ToDouble(CalifAbri[i]);
                }

                PromAbrTemp = PromAbrTemp / 9;
                PromAbr = PromAbrTemp.ToString("0.#");//Promedio de abril

                double PromAbriGen = 0;

                PromAbriGen = Convert.ToDouble(PromAbr) + Convert.ToDouble(CalifAbri[9]);
                PromAbriGen = PromAbriGen / 2;
                PromAbriGeneral = PromAbriGen.ToString("0.#"); //Promedio general de abril

            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifAbri[i] = " ";
                }
            }
            conn8.Close();
            //Mayo-----------------------------------------------------------------
            string CalifMay = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Mayo'";

            MySqlConnection conn9;
            MySqlCommand com9;

            conn9 = new MySqlConnection(conexion);
            conn9.Open();

            com9 = new MySqlCommand(CalifMay, conn9);

            MySqlDataReader myreader9 = com9.ExecuteReader();

            string[] CalifMayo = new string[11];
            string PromMay = " ";
            string PromMayoGeneral = " ";

            if (myreader9.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader9.Read())//Agrega calificaciones
                {
                    double[] CalifMayotemp = new double[11];
                    CalifMayotemp[L] = Convert.ToDouble(myreader9["CalificacionMen"]);
                    CalifMayo[L] = CalifMayotemp[L].ToString("0.#");
                    L++;
                }

                double PromMayTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromMayTemp = PromMayTemp + Convert.ToDouble(CalifMayo[i]);
                }

                PromMayTemp = PromMayTemp / 9;
                PromMay = PromMayTemp.ToString("0.#");//Promedio de mayo

                double PromMayoGen = 0;

                PromMayoGen = Convert.ToDouble(PromMay) + Convert.ToDouble(CalifMayo[9]);
                PromMayoGen = PromMayoGen / 2;
                PromMayoGeneral = PromMayoGen.ToString("0.#"); //Promedio general de mayo
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifMayo[i] = " ";
                }
            }
            conn9.Close();
            //Junio-----------------------------------------------------------------
            string CalifJun = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Junio'";

            MySqlConnection conn10;
            MySqlCommand com10;

            conn10 = new MySqlConnection(conexion);
            conn10.Open();

            com10 = new MySqlCommand(CalifJun, conn10);

            MySqlDataReader myreader10 = com10.ExecuteReader();

            string[] CalifJuni = new string[11];
            string PromJun = " ";
            string PromTriEsp3 = " ";
            string PromTriMat3 = " ";
            string PromTriIng3 = " ";
            string PromTricn3 = " ";
            string PromTriGeo3 = " ";
            string PromTriHis3 = " ";
            string PromTriCiv3 = " ";
            string PromTriArt3 = " ";
            string PromTriFis3 = " ";
            string PromTriSoc3 = " ";
            string SumaInasisTri3 = " ";
            string Nivel3 = " ";
            string NivelMat3 = " ";
            string NivelIng3 = " ";
            string Nivelcn3 = " ";
            string NivelGeo3 = " ";
            string NivelHis3 = " ";
            string NivelCiv3 = " ";
            string NivelArt3 = " ";
            string NivelFis3 = " ";
            string NivelSoc3 = " ";
            string PromTriGen3 = " ";
            string NivelGen3 = " ";
            string PromFinTriEsp = " ";
            string NivelFin = " ";
            string PromFinTriMat = " ";
            string NivelMatFin = " ";
            string PromFinTriIng = " ";
            string NivelIngFin = " ";
            string PromFinTricn = " ";
            string NivelcnFin = " ";
            string PromFinTriGeo = " ";
            string NivelGeoFin = " ";
            string PromFinTriHis = " ";
            string NivelHisFin = " ";
            string PromFinTriCiv = " ";
            string NivelCivFin = " ";
            string PromFinTriArt = " ";
            string NivelArtFin = " ";
            string PromFinTriFis = " ";
            string NivelFisFin = " ";
            string PromFinTriSoc = " ";
            string NivelSocFin = " ";
            string PromTriSen3 = " ";
            string NivelPromSen3 = " ";
            string PromTriSenFin = " ";
            string NivelPromSenFin = " ";
            string PromTriGenFin = " ";
            string NivelGenFin = " ";
            string SumInasisTriFin = " ";

            string PromJuniGeneral = " ";

            if (myreader10.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader10.Read())//Agrega calificaciones
                {
                    double[] CalifJunitemp = new double[11];
                    CalifJunitemp[L] = Convert.ToDouble(myreader10["CalificacionMen"]);
                    CalifJuni[L] = CalifJunitemp[L].ToString("0.#");
                    L++;
                }

                double PromJunTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromJunTemp = PromJunTemp + Convert.ToDouble(CalifJuni[i]);
                }

                PromJunTemp = PromJunTemp / 9;
                PromJun = PromJunTemp.ToString("0.#");//Promedio de junio

                double PromJuniGen = 0;

                PromJuniGen = Convert.ToDouble(PromJun) + Convert.ToDouble(CalifJuni[9]);
                PromJuniGen = PromJuniGen / 2;
                PromJuniGeneral = PromJuniGen.ToString("0.#"); //Promedio general de junio
                
                //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                double PromSen33 = 0;

                PromSen33 = Convert.ToDouble(PromMar) + Convert.ToDouble(PromAbr) + Convert.ToDouble(PromMay) + Convert.ToDouble(PromJun);
                PromSen33 = PromSen33 / 4;
                PromTriSen3 = PromSen33.ToString("0.#");//Promedio del segundo trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriSen3) >= 5 && Convert.ToDouble(PromTriSen3) < 6)
                {
                    NivelPromSen3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSen3) >= 6 && Convert.ToDouble(PromTriSen3) < 8)
                    {
                        NivelPromSen3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen3) >= 8 && Convert.ToDouble(PromTriSen3) < 10)
                        {
                            NivelPromSen3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen3) == 10)
                            {
                                NivelPromSen3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelPromSen3 = " ";
                            }
                        }
                    }
                }

                //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                double PromGen33 = 0;

                PromGen33 = Convert.ToDouble(PromMarzGeneral) + Convert.ToDouble(PromAbriGeneral) + Convert.ToDouble(PromMayoGeneral) + Convert.ToDouble(PromJuniGeneral);
                PromGen33 = PromGen33 / 4;
                PromTriGen3 = PromGen33.ToString("0.#");//Promedio del primer trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriGen3) >= 5 && Convert.ToDouble(PromTriGen3) < 6)
                {
                    NivelGen3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGen3) >= 6 && Convert.ToDouble(PromTriGen3) < 8)
                    {
                        NivelGen3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen3) >= 8 && Convert.ToDouble(PromTriGen3) < 10)
                        {
                            NivelGen3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen3) == 10)
                            {
                                NivelGen3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGen3 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Español-------------------------------------------------

                double PromEsp33 = 0;

                PromEsp33 = Convert.ToDouble(CalifMarz[0]) + Convert.ToDouble(CalifAbri[0]) + Convert.ToDouble(CalifMayo[0]) + Convert.ToDouble(CalifJuni[0]);
                PromEsp33 = PromEsp33 / 4;
                PromTriEsp3 = PromEsp33.ToString("0.#");//Promedio del tercer trimestre de español

                //Asignacion del nivel del tercer trimestre de español
                if (Convert.ToDouble(PromTriEsp3) >= 5 && Convert.ToDouble(PromTriEsp3) < 6)
                {
                    Nivel3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriEsp3) >= 6 && Convert.ToDouble(PromTriEsp3) < 8)
                    {
                        Nivel3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp3) >= 8 && Convert.ToDouble(PromTriEsp3) < 10)
                        {
                            Nivel3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp3) == 10)
                            {
                                Nivel3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivel3 = " ";
                            }
                        }
                    }
                }

                //-------------------------------------Matematicas--------------------------------------------------------
                double PromMat33 = 0;

                PromMat33 = Convert.ToDouble(CalifMarz[1]) + Convert.ToDouble(CalifAbri[1]) + Convert.ToDouble(CalifMayo[1]) + Convert.ToDouble(CalifJuni[1]);
                PromMat33 = PromMat33 / 4;
                PromTriMat3 = PromMat33.ToString("0.#");//Promedio del primer trimestre de matematicas

                //Asignacion del nivel del primer trimestre de matematicas
                if (Convert.ToDouble(PromTriMat3) >= 5 && Convert.ToDouble(PromTriMat3) < 6)
                {
                    NivelMat3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriMat3) >= 6 && Convert.ToDouble(PromTriMat3) < 8)
                    {
                        NivelMat3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat3) >= 8 && Convert.ToDouble(PromTriMat3) < 10)
                        {
                            NivelMat3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat3) == 10)
                            {
                                NivelMat3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelMat3 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ingles-------------------------------------------------
                double PromIng33 = 0;

                PromIng33 = Convert.ToDouble(CalifMarz[2]) + Convert.ToDouble(CalifAbri[2]) + Convert.ToDouble(CalifMayo[2]) + Convert.ToDouble(CalifJuni[2]);
                PromIng33 = PromIng33 / 4;
                PromTriIng3 = PromIng33.ToString("0.#");//Promedio del tercer trimestre de Ingles

                //Asignacion del nivel del tercer trimestre de Ingles
                if (Convert.ToDouble(PromTriIng3) >= 5 && Convert.ToDouble(PromTriIng3) < 6)
                {
                    NivelIng3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriIng3) >= 6 && Convert.ToDouble(PromTriIng3) < 8)
                    {
                        NivelIng3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng3) >= 8 && Convert.ToDouble(PromTriIng3) < 10)
                        {
                            NivelIng3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng3) == 10)
                            {
                                NivelIng3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelIng3 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                double Promcn33 = 0;

                Promcn33 = Convert.ToDouble(CalifMarz[3]) + Convert.ToDouble(CalifAbri[3]) + Convert.ToDouble(CalifMayo[3]) + Convert.ToDouble(CalifJuni[3]);
                Promcn33 = Promcn33 / 4;
                PromTricn3 = Promcn33.ToString("0.#");//Promedio del tercer trimestre de Ciencias

                //Asignacion del nivel del tercer trimestre de Ciencias
                if (Convert.ToDouble(PromTricn3) >= 5 && Convert.ToDouble(PromTricn3) < 6)
                {
                    Nivelcn3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTricn3) >= 6 && Convert.ToDouble(PromTricn3) < 8)
                    {
                        Nivelcn3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn3) >= 8 && Convert.ToDouble(PromTricn3) < 10)
                        {
                            Nivelcn3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn3) == 10)
                            {
                                Nivelcn3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                Nivelcn3 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Geografia-------------------------------------------------
                double PromGeo33 = 0;

                PromGeo33 = Convert.ToDouble(CalifMarz[4]) + Convert.ToDouble(CalifAbri[4]) + Convert.ToDouble(CalifMayo[4]) + Convert.ToDouble(CalifJuni[4]);
                PromGeo33 = PromGeo33 / 4;
                PromTriGeo3 = PromGeo33.ToString("0.#");//Promedio del tercer trimestre de Geografia

                //Asignacion del nivel del tercer trimestre de Geografia
                if (Convert.ToDouble(PromTriGeo3) >= 5 && Convert.ToDouble(PromTriGeo3) < 6)
                {
                    NivelGeo3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGeo3) >= 6 && Convert.ToDouble(PromTriGeo3) < 8)
                    {
                        NivelGeo3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo3) >= 8 && Convert.ToDouble(PromTriGeo3) < 10)
                        {
                            NivelGeo3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo3) == 10)
                            {
                                NivelGeo3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGeo3 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Historia--------------------------------------------------------
                double PromHis33 = 0;

                PromHis33 = Convert.ToDouble(CalifMarz[5]) + Convert.ToDouble(CalifAbri[5]) + Convert.ToDouble(CalifMayo[5]) + Convert.ToDouble(CalifJuni[5]);
                PromHis33 = PromHis33 / 4;
                PromTriHis3 = PromHis33.ToString("0.#");//Promedio del tercer trimestre de Historia

                //Asignacion del nivel del tercer trimestre de Historia
                if (Convert.ToDouble(PromTriHis3) >= 5 && Convert.ToDouble(PromTriHis3) < 6)
                {
                    NivelHis3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriHis3) >= 6 && Convert.ToDouble(PromTriHis3) < 8)
                    {
                        NivelHis3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis3) >= 8 && Convert.ToDouble(PromTriHis3) < 10)
                        {
                            NivelHis3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis3) == 10)
                            {
                                NivelHis3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelHis3 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Formacion Civica y etica-------------------------------------------------
                double PromCiv33 = 0;

                PromCiv33 = Convert.ToDouble(CalifMarz[6]) + Convert.ToDouble(CalifAbri[6]) + Convert.ToDouble(CalifMayo[6]) + Convert.ToDouble(CalifJuni[6]);
                PromCiv33 = PromCiv33 / 4;
                PromTriCiv3 = PromCiv33.ToString("0.#");//Promedio del tercer trimestre de Formacion Civica y etica

                //Asignacion del nivel del tercer trimestre de Formacion Civica y etica
                if (Convert.ToDouble(PromTriCiv3) >= 5 && Convert.ToDouble(PromTriCiv3) < 6)
                {
                    NivelCiv3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCiv3) >= 6 && Convert.ToDouble(PromTriCiv3) < 8)
                    {
                        NivelCiv3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv3) >= 8 && Convert.ToDouble(PromTriCiv3) < 10)
                        {
                            NivelCiv3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv3) == 10)
                            {
                                NivelCiv3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCiv3 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt33 = 0;

                PromArt33 = Convert.ToDouble(CalifMarz[7]) + Convert.ToDouble(CalifAbri[7]) + Convert.ToDouble(CalifMayo[7]) + Convert.ToDouble(CalifJuni[7]);
                PromArt33 = PromArt33 / 4;
                PromTriArt3 = PromArt33.ToString("0.#");//Promedio del tercer trimestre de Artes
                
                //Asignacion del nivel del tercer trimestre de Artes
                if (Convert.ToDouble(PromTriArt3) >= 5 && Convert.ToDouble(PromTriArt3) < 6)
                {
                    NivelArt3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriArt3) >= 6 && Convert.ToDouble(PromTriArt3) < 8)
                    {
                        NivelArt3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt3) >= 8 && Convert.ToDouble(PromTriArt3) < 10)
                        {
                            NivelArt3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt3) == 10)
                            {
                                NivelArt3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelArt3 = " ";
                            }
                        }
                    }
                }
                //------------------------------------Ed. Fisica-------------------------------------------------
                double PromFis33 = 0;

                PromFis33 = Convert.ToDouble(CalifMarz[8]) + Convert.ToDouble(CalifAbri[8]) + Convert.ToDouble(CalifMayo[8]) + Convert.ToDouble(CalifJuni[8]);
                PromFis33 = PromFis33 / 4;
                PromTriFis3 = PromFis33.ToString("0.#");//Promedio del tercer trimestre de Ed. Fisica

                //Asignacion del nivel del tercer trimestre de Ed. Fisica
                if (Convert.ToDouble(PromTriFis3) >= 5 && Convert.ToDouble(PromTriFis3) < 6)
                {
                    NivelFis3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriFis3) >= 6 && Convert.ToDouble(PromTriFis3) < 8)
                    {
                        NivelFis3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis3) >= 8 && Convert.ToDouble(PromTriFis3) < 10)
                        {
                            NivelFis3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis3) == 10)
                            {
                                NivelFis3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelFis3 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                double PromSoc33 = 0;

                PromSoc33 = Convert.ToDouble(CalifMarz[9]) + Convert.ToDouble(CalifAbri[9]) + Convert.ToDouble(CalifMayo[9]) + Convert.ToDouble(CalifJuni[9]);
                PromSoc33 = PromSoc33 / 4;
                PromTriSoc3 = PromSoc33.ToString("0.#");//Promedio del tercer trimestre de Ed. Socioemocional

                //Asignacion del nivel del tercer trimestre de Ed. Socioemocional
                if (Convert.ToDouble(PromTriSoc3) >= 5 && Convert.ToDouble(PromTriSoc3) < 6)
                {
                    NivelSoc3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSoc3) >= 6 && Convert.ToDouble(PromTriSoc3) < 8)
                    {
                        NivelSoc3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc3) >= 8 && Convert.ToDouble(PromTriSoc3) < 10)
                        {
                            NivelSoc3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc3) == 10)
                            {
                                NivelSoc3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelSoc3 = " ";
                            }
                        }
                    }
                }

                //-----------------------------Inasistencia-----------------------------------------------------
                double SumaInasis33 = 0;

                SumaInasis33 = Convert.ToDouble(CalifMarz[10]) + Convert.ToDouble(CalifAbri[10]) + Convert.ToDouble(CalifMayo[10]) + Convert.ToDouble(CalifJuni[10]);
                SumaInasisTri3 = SumaInasis33.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                //------------------------------------Promedio de materias sencillas final trimestres-------------------------------------------------
                double PromSenFin = 0;

                PromSenFin = Convert.ToDouble(PromTriSen1) + Convert.ToDouble(PromTriSen2) + Convert.ToDouble(PromTriSen3);
                PromSenFin = PromSenFin / 3;
                PromTriSenFin = PromSenFin.ToString("0.#");//Promedio del segundo trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriSenFin) >= 5 && Convert.ToDouble(PromTriSenFin) < 6)
                {
                    NivelPromSenFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriSenFin) >= 6 && Convert.ToDouble(PromTriSenFin) < 8)
                    {
                        NivelPromSenFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSenFin) >= 8 && Convert.ToDouble(PromTriSenFin) < 10)
                        {
                            NivelPromSenFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSenFin) == 10)
                            {
                                NivelPromSenFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelPromSenFin = " ";
                            }
                        }
                    }
                }

                //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                double PromGen3Fin = 0;

                PromGen3Fin = Convert.ToDouble(PromTriGen1) + Convert.ToDouble(PromTriGen2) + Convert.ToDouble(PromTriGen3);
                PromGen3Fin = PromGen3Fin / 3;
                PromTriGenFin = PromGen3Fin.ToString("0.#");//Promedio del primer trimestre 

                //Asignacion del nivel del primer trimestre 
                if (Convert.ToDouble(PromTriGenFin) >= 5 && Convert.ToDouble(PromTriGenFin) < 6)
                {
                    NivelGenFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriGenFin) >= 6 && Convert.ToDouble(PromTriGenFin) < 8)
                    {
                        NivelGenFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGenFin) >= 8 && Convert.ToDouble(PromTriGenFin) < 10)
                        {
                            NivelGenFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGenFin) == 10)
                            {
                                NivelGenFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGenFin = " ";
                            }
                        }
                    }
                }

                //--------------------------------------Promedio final de español trimestral--------------------------------
                double PromFin = 0;

                PromFin = Convert.ToDouble(PromTriEsp1) + Convert.ToDouble(PromTriEsp2) + Convert.ToDouble(PromTriEsp3);
                PromFin = PromFin / 3;
                PromFinTriEsp = PromFin.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriEsp) >= 5 && Convert.ToDouble(PromFinTriEsp) < 6)
                {
                    NivelFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriEsp) >= 6 && Convert.ToDouble(PromFinTriEsp) < 8)
                    {
                        NivelFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriEsp) >= 8 && Convert.ToDouble(PromFinTriEsp) < 10)
                        {
                            NivelFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriEsp) == 10)
                            {
                                NivelFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelFin = " ";
                            }
                        }
                    }
                }

                //--------------------------------------Promedio final de matematicas trimestral--------------------------------
                double PromFinMat = 0;

                PromFinMat = Convert.ToDouble(PromTriMat1) + Convert.ToDouble(PromTriMat2) + Convert.ToDouble(PromTriMat3);
                PromFinMat = PromFinMat / 3;
                PromFinTriMat = PromFinMat.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriMat) >= 5 && Convert.ToDouble(PromFinTriMat) < 6)
                {
                    NivelMatFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriMat) >= 6 && Convert.ToDouble(PromFinTriMat) < 8)
                    {
                        NivelMatFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriMat) >= 8 && Convert.ToDouble(PromFinTriMat) < 10)
                        {
                            NivelMatFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriMat) == 10)
                            {
                                NivelMatFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelMatFin = " ";
                            }
                        }
                    }
                }

                //--------------------------------------Promedio final de Ingles trimestral--------------------------------
                double PromFinIng = 0;

                PromFinIng = Convert.ToDouble(PromTriIng1) + Convert.ToDouble(PromTriIng2) + Convert.ToDouble(PromTriIng3);
                PromFinIng = PromFinIng / 3;
                PromFinTriIng = PromFinIng.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriIng) >= 5 && Convert.ToDouble(PromFinTriIng) < 6)
                {
                    NivelIngFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriIng) >= 6 && Convert.ToDouble(PromFinTriIng) < 8)
                    {
                        NivelIngFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriIng) >= 8 && Convert.ToDouble(PromFinTriIng) < 10)
                        {
                            NivelIngFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriIng) == 10)
                            {
                                NivelIngFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelIngFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Ciencias Naturales trimestral--------------------------------
                double PromFincn = 0;

                PromFincn = Convert.ToDouble(PromTricn1) + Convert.ToDouble(PromTricn2) + Convert.ToDouble(PromTricn3);
                PromFincn = PromFincn / 3;
                PromFinTricn = PromFincn.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTricn) >= 5 && Convert.ToDouble(PromFinTricn) < 6)
                {
                    NivelcnFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTricn) >= 6 && Convert.ToDouble(PromFinTricn) < 8)
                    {
                        NivelcnFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTricn) >= 8 && Convert.ToDouble(PromFinTricn) < 10)
                        {
                            NivelcnFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTricn) == 10)
                            {
                                NivelcnFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelcnFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Geografia trimestral--------------------------------
                double PromFinGeo = 0;

                PromFinGeo = Convert.ToDouble(PromTriGeo1) + Convert.ToDouble(PromTriGeo2) + Convert.ToDouble(PromTriGeo3);
                PromFinGeo = PromFinGeo / 3;
                PromFinTriGeo = PromFinGeo.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriGeo) >= 5 && Convert.ToDouble(PromFinTriGeo) < 6)
                {
                    NivelGeoFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriGeo) >= 6 && Convert.ToDouble(PromFinTriGeo) < 8)
                    {
                        NivelGeoFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriGeo) >= 8 && Convert.ToDouble(PromFinTriGeo) < 10)
                        {
                            NivelGeoFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriGeo) == 10)
                            {
                                NivelGeoFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelGeoFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Historia trimestral--------------------------------
                double PromFinHis = 0;

                PromFinHis = Convert.ToDouble(PromTriHis1) + Convert.ToDouble(PromTriHis2) + Convert.ToDouble(PromTriHis3);
                PromFinHis = PromFinHis / 3;
                PromFinTriHis = PromFinHis.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriHis) >= 5 && Convert.ToDouble(PromFinTriHis) < 6)
                {
                    NivelHisFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriHis) >= 6 && Convert.ToDouble(PromFinTriHis) < 8)
                    {
                        NivelHisFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriHis) >= 8 && Convert.ToDouble(PromFinTriHis) < 10)
                        {
                            NivelHisFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriHis) == 10)
                            {
                                NivelHisFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelHisFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Formacion Civica y etica trimestral--------------------------------
                double PromFinCiv = 0;

                PromFinCiv = Convert.ToDouble(PromTriCiv1) + Convert.ToDouble(PromTriCiv2) + Convert.ToDouble(PromTriCiv3);
                PromFinCiv = PromFinCiv / 3;
                PromFinTriCiv = PromFinCiv.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriCiv) >= 5 && Convert.ToDouble(PromFinTriCiv) < 6)
                {
                    NivelCivFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriCiv) >= 6 && Convert.ToDouble(PromFinTriCiv) < 8)
                    {
                        NivelCivFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriCiv) >= 8 && Convert.ToDouble(PromFinTriCiv) < 10)
                        {
                            NivelCivFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriCiv) == 10)
                            {
                                NivelCivFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCivFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Artes trimestral--------------------------------
                double PromFinArt = 0;

                PromFinArt = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriArt3);
                PromFinArt = PromFinArt / 3;
                PromFinTriArt = PromFinArt.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriArt) >= 5 && Convert.ToDouble(PromFinTriArt) < 6)
                {
                    NivelArtFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriArt) >= 6 && Convert.ToDouble(PromFinTriArt) < 8)
                    {
                        NivelArtFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriArt) >= 8 && Convert.ToDouble(PromFinTriArt) < 10)
                        {
                            NivelArtFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriArt) == 10)
                            {
                                NivelArtFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelArtFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Ed. Fisica trimestral--------------------------------
                double PromFinFis = 0;

                PromFinFis = Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriFis3);
                PromFinFis = PromFinFis / 3;
                PromFinTriFis = PromFinFis.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriFis) >= 5 && Convert.ToDouble(PromFinTriFis) < 6)
                {
                    NivelFisFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriFis) >= 6 && Convert.ToDouble(PromFinTriFis) < 8)
                    {
                        NivelFisFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriFis) >= 8 && Convert.ToDouble(PromFinTriFis) < 10)
                        {
                            NivelFisFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriFis) == 10)
                            {
                                NivelFisFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelFisFin = " ";
                            }
                        }
                    }
                }
                //--------------------------------------Promedio final de Ed. Socioemocional trimestral--------------------------------
                double PromFinSoc = 0;

                PromFinSoc = Convert.ToDouble(PromTriSoc1) + Convert.ToDouble(PromTriSoc2) + Convert.ToDouble(PromTriSoc3);
                PromFinSoc = PromFinSoc / 3;
                PromFinTriSoc = PromFinSoc.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriSoc) >= 5 && Convert.ToDouble(PromFinTriSoc) < 6)
                {
                    NivelSocFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriSoc) >= 6 && Convert.ToDouble(PromFinTriSoc) < 8)
                    {
                        NivelSocFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriSoc) >= 8 && Convert.ToDouble(PromFinTriSoc) < 10)
                        {
                            NivelSocFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriSoc) == 10)
                            {
                                NivelSocFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelSocFin = " ";
                            }
                        }
                    }
                }

                //-----------------------------Numero final de Inasistencia-----------------------------------------------------
                double SumaInasisFin = 0;

                SumaInasisFin = Convert.ToDouble(SumaInasisTri1) + Convert.ToDouble(SumaInasisTri2) + Convert.ToDouble(SumaInasisTri3);
                SumInasisTriFin = SumaInasisFin.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifJuni[i] = " ";
                }
            }
            conn10.Close();
            //Diagnostico-----------------------------------------------------------------
            string CalifDig = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Diagnostico'";

            MySqlConnection conn11;
            MySqlCommand com11;

            conn11 = new MySqlConnection(conexion);
            conn11.Open();

            com11 = new MySqlCommand(CalifDig, conn11);

            MySqlDataReader myreader11 = com11.ExecuteReader();

            string[] CalifDiag = new string[11];
            string PromDiag = " ";
            string PromDiagGeneral = " ";

            if (myreader11.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader11.Read())//Agrega calificaciones
                {
                    double[] CalifDiagtemp = new double[11];
                    CalifDiagtemp[L] = Convert.ToDouble(myreader11["CalificacionMen"]);
                    CalifDiag[L] = CalifDiagtemp[L].ToString("0.#");
                    L++;
                }

                double PromDiagTemp = 0;

                for (int i = 0; i < 9; i++)
                {
                    PromDiagTemp = PromDiagTemp + Convert.ToDouble(CalifDiag[i]);
                }

                PromDiagTemp = PromDiagTemp / 9;
                PromDiag = PromDiagTemp.ToString("0.#");//Promedio de diagnostico

                double PromDiagGen = 0;

                PromDiagGen = Convert.ToDouble(PromDiag) + Convert.ToDouble(CalifDiag[9]);
                PromDiagGen = PromDiagGen / 2;
                PromDiagGeneral = PromDiagGen.ToString("0.#"); //Promedio general de diagnostico
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    CalifDiag[i] = " ";
                }
            }
            conn11.Close();

            //-------------------------------------------------------------------------------------------------------------------
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            // Creamos el documento con el tamaño de página tradicional
            FileStream stream = new FileStream(folderPath + "Boleta-Interna456.pdf", FileMode.Create);
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
            float[] Celdas = { 1.50f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.25f, 0.35f, 0.25f, 0.35f, 0.25f, 0.35f, 0.25f, 0.35f, 0.25f };

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
            table.AddCell("" + PromTriEsp1);
            table.AddCell("" + Nivel1);
            table.AddCell("" + PromTriEsp2);
            table.AddCell("" + Nivel2);
            table.AddCell("" + PromTriEsp3);
            table.AddCell("" + Nivel3);
            table.AddCell("" + PromFinTriEsp);
            table.AddCell("" + NivelFin);

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
            table.AddCell("" + PromTriMat1);
            table.AddCell("" + NivelMat1);
            table.AddCell("" + PromTriMat2);
            table.AddCell("" + NivelMat2);
            table.AddCell("" + PromTriMat3);
            table.AddCell("" + NivelMat3);
            table.AddCell("" + PromFinTriMat);
            table.AddCell("" + NivelMatFin);

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
            table.AddCell("" + PromTriIng1);
            table.AddCell("" + NivelIng1);
            table.AddCell("" + PromTriIng2);
            table.AddCell("" + NivelIng2);
            table.AddCell("" + PromTriIng3);
            table.AddCell("" + NivelIng3);
            table.AddCell("" + PromFinTriIng);
            table.AddCell("" + NivelIngFin);

            PdfPCell cell6 = new PdfPCell(new Phrase("CIENCIAS NATURALES", cuerpo));
            cell6.Colspan = 2;
            table.AddCell(cell6);

            table.AddCell("" + CalifDiag[3]);
            table.AddCell("" + CalifSept[3]);
            table.AddCell("" + CalifOctu[3]);
            table.AddCell("" + CalifNovi[3]);
            table.AddCell("" + CalifDici[3]);
            table.AddCell("" + CalifEner[3]);
            table.AddCell("" + CalifFebr[3]);
            table.AddCell("" + CalifMarz[3]);
            table.AddCell("" + CalifAbri[3]);
            table.AddCell("" + CalifMayo[3]);
            table.AddCell("" + CalifJuni[3]);

            //Espacios linea de español
            table.AddCell("" + PromTricn1);
            table.AddCell("" + Nivelcn1);
            table.AddCell("" + PromTricn2);
            table.AddCell("" + Nivelcn2);
            table.AddCell("" + PromTricn3);
            table.AddCell("" + Nivelcn3);
            table.AddCell("" + PromFinTricn);
            table.AddCell("" + NivelcnFin);

            PdfPCell cell50 = new PdfPCell(new Phrase("GEOGRAFÍA", cuerpo));
            cell50.Colspan = 2;
            table.AddCell(cell50);

            table.AddCell("" + CalifDiag[4]);
            table.AddCell("" + CalifSept[4]);
            table.AddCell("" + CalifOctu[4]);
            table.AddCell("" + CalifNovi[4]);
            table.AddCell("" + CalifDici[4]);
            table.AddCell("" + CalifEner[4]);
            table.AddCell("" + CalifFebr[4]);
            table.AddCell("" + CalifMarz[4]);
            table.AddCell("" + CalifAbri[4]);
            table.AddCell("" + CalifMayo[4]);
            table.AddCell("" + CalifJuni[4]);

            //Espacios linea de español
            table.AddCell("" + PromTriGeo1);
            table.AddCell("" + NivelGeo1);
            table.AddCell("" + PromTriGeo2);
            table.AddCell("" + NivelGeo2);
            table.AddCell("" + PromTriGeo3);
            table.AddCell("" + NivelGeo3);
            table.AddCell("" + PromFinTriGeo);
            table.AddCell("" + NivelGeoFin);

            PdfPCell cell52 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
            cell52.Colspan = 2;
            table.AddCell(cell52);

            table.AddCell("" + CalifDiag[5]);
            table.AddCell("" + CalifSept[5]);
            table.AddCell("" + CalifOctu[5]);
            table.AddCell("" + CalifNovi[5]);
            table.AddCell("" + CalifDici[5]);
            table.AddCell("" + CalifEner[5]);
            table.AddCell("" + CalifFebr[5]);
            table.AddCell("" + CalifMarz[5]);
            table.AddCell("" + CalifAbri[5]);
            table.AddCell("" + CalifMayo[5]);
            table.AddCell("" + CalifJuni[5]);

            //Espacios linea de español
            table.AddCell("" + PromTriHis1);
            table.AddCell("" + NivelHis1);
            table.AddCell("" + PromTriHis2);
            table.AddCell("" + NivelHis2);
            table.AddCell("" + PromTriHis3);
            table.AddCell("" + NivelHis3);
            table.AddCell("" + PromFinTriHis);
            table.AddCell("" + NivelHisFin);


            PdfPCell cell51 = new PdfPCell(new Phrase("FORMACIÓN CÍVICA Y ÉTICA", cuerpo));
            cell51.Colspan = 2;
            table.AddCell(cell51);

            table.AddCell("" + CalifDiag[6]);
            table.AddCell("" + CalifSept[6]);
            table.AddCell("" + CalifOctu[6]);
            table.AddCell("" + CalifNovi[6]);
            table.AddCell("" + CalifDici[6]);
            table.AddCell("" + CalifEner[6]);
            table.AddCell("" + CalifFebr[6]);
            table.AddCell("" + CalifMarz[6]);
            table.AddCell("" + CalifAbri[6]);
            table.AddCell("" + CalifMayo[6]);
            table.AddCell("" + CalifJuni[6]);

            //Espacios linea de español
            table.AddCell("" + PromTriCiv1);
            table.AddCell("" + NivelCiv1);
            table.AddCell("" + PromTriCiv2);
            table.AddCell("" + NivelCiv2);
            table.AddCell("" + PromTriCiv3);
            table.AddCell("" + NivelCiv3);
            table.AddCell("" + PromFinTriCiv);
            table.AddCell("" + NivelCivFin);

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

            table.AddCell("" + CalifDiag[7]);
            table.AddCell("" + CalifSept[7]);
            table.AddCell("" + CalifOctu[7]);
            table.AddCell("" + CalifNovi[7]);
            table.AddCell("" + CalifDici[7]);
            table.AddCell("" + CalifEner[7]);
            table.AddCell("" + CalifFebr[7]);
            table.AddCell("" + CalifMarz[7]);
            table.AddCell("" + CalifAbri[7]);
            table.AddCell("" + CalifMayo[7]);
            table.AddCell("" + CalifJuni[7]);

            //Espacios linea de español
            table.AddCell("" + PromTriArt1);
            table.AddCell("" + NivelArt1);
            table.AddCell("" + PromTriArt2);
            table.AddCell("" + NivelArt2);
            table.AddCell("" + PromTriArt3);
            table.AddCell("" + NivelArt3);
            table.AddCell("" + PromFinTriArt);
            table.AddCell("" + NivelArtFin);

            PdfPCell cell9 = new PdfPCell(new Phrase("EDUCACIÓN FÍSICA", cuerpo));
            cell9.Colspan = 2;
            table.AddCell(cell9);

            table.AddCell("" + CalifDiag[8]);
            table.AddCell("" + CalifSept[8]);
            table.AddCell("" + CalifOctu[8]);
            table.AddCell("" + CalifNovi[8]);
            table.AddCell("" + CalifDici[8]);
            table.AddCell("" + CalifEner[8]);
            table.AddCell("" + CalifFebr[8]);
            table.AddCell("" + CalifMarz[8]);
            table.AddCell("" + CalifAbri[8]);
            table.AddCell("" + CalifMayo[8]);
            table.AddCell("" + CalifJuni[8]);

            //Espacios linea de español
            table.AddCell("" + PromTriFis1);
            table.AddCell("" + NivelFis1);
            table.AddCell("" + PromTriFis2);
            table.AddCell("" + NivelFis2);
            table.AddCell("" + PromTriFis3);
            table.AddCell("" + NivelFis3);
            table.AddCell("" + PromFinTriFis);
            table.AddCell("" + NivelFisFin);

            PdfPCell cell10 = new PdfPCell(new Phrase("PROM. FINAL FORMACIÓN ACADÉMICA", letchica));
            cell10.Colspan = 2;
            cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell10);

            table.AddCell("" + PromDiag);
            table.AddCell("" + PromSept);
            table.AddCell("" + PromOct);
            table.AddCell("" + PromNov);
            table.AddCell("" + PromDic);
            table.AddCell("" + PromEne);
            table.AddCell("" + PromFeb);
            table.AddCell("" + PromMar);
            table.AddCell("" + PromAbr);
            table.AddCell("" + PromMay);
            table.AddCell("" + PromJun);

            //Espacios linea de español
            table.AddCell("" + PromTriSen1);
            table.AddCell("" + NivelPromSen);
            table.AddCell("" + PromTriSen2);
            table.AddCell("" + NivelPromSen2);
            table.AddCell("" + PromTriSen3);
            table.AddCell("" + NivelPromSen3);
            table.AddCell("" + PromTriSenFin);
            table.AddCell("" + NivelPromSenFin);

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
            table.AddCell("" + CalifDici[9]);
            table.AddCell("" + CalifEner[9]);
            table.AddCell("" + CalifFebr[9]);
            table.AddCell("" + CalifMarz[9]);
            table.AddCell("" + CalifAbri[9]);
            table.AddCell("" + CalifMayo[9]);
            table.AddCell("" + CalifJuni[9]);

            //Espacios linea de español
            table.AddCell("" + PromTriSoc1);
            table.AddCell("" + NivelSoc1);
            table.AddCell("" + PromTriSoc2);
            table.AddCell("" + NivelSoc2);
            table.AddCell("" + PromTriSoc3);
            table.AddCell("" + NivelSoc3);
            table.AddCell("" + PromFinTriSoc);
            table.AddCell("" + NivelSocFin);

            PdfPCell cell14 = new PdfPCell(new Phrase("PROM. FINAL DE LAS ÁREAS DESARROLLO PERSONAL Y SOCIAL FORMACIÓN ACADÉMICA", letchica));
            cell14.Colspan = 2;
            cell14.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell14);

            table.AddCell("" + CalifDiag[9]);
            table.AddCell("" + CalifSept[9]);
            table.AddCell("" + CalifOctu[9]);
            table.AddCell("" + CalifNovi[9]);
            table.AddCell("" + CalifDici[9]);
            table.AddCell("" + CalifEner[9]);
            table.AddCell("" + CalifFebr[9]);
            table.AddCell("" + CalifMarz[9]);
            table.AddCell("" + CalifAbri[9]);
            table.AddCell("" + CalifMayo[9]);
            table.AddCell("" + CalifJuni[9]);

            //Espacios linea de español
            table.AddCell("" + PromTriSoc1);
            table.AddCell("" + NivelSoc1);
            table.AddCell("" + PromTriSoc2);
            table.AddCell("" + NivelSoc2);
            table.AddCell("" + PromTriSoc3);
            table.AddCell("" + NivelSoc3);
            table.AddCell("" + PromFinTriSoc);
            table.AddCell("" + NivelSocFin);

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

            table.AddCell("" + PromDiagGeneral);
            table.AddCell("" + PromSeptGeneral);
            table.AddCell("" + PromOctuGeneral);
            table.AddCell("" + PromNoviGeneral);
            table.AddCell("" + PromDiciGeneral);
            table.AddCell("" + PromEnerGeneral);
            table.AddCell("" + PromFebrGeneral);
            table.AddCell("" + PromMarzGeneral);
            table.AddCell("" + PromAbriGeneral);
            table.AddCell("" + PromMayoGeneral);
            table.AddCell("" + PromJuniGeneral);
            table.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha

            //tabla vacia vertical
            PdfPCell cell1i = new PdfPCell(new Phrase(" ", cuerpo));
            cell1i.Rowspan = 3;
            cell1i.BorderWidth = 0;
            table.AddCell(cell1i);

            //Espacios linea de español
            table.AddCell("" + PromTriGen1);
            table.AddCell("" + NivelGen1);
            table.AddCell("" + PromTriGen2);
            table.AddCell("" + NivelGen2);
            table.AddCell("" + PromTriGen3);
            table.AddCell("" + NivelGen3);
            table.AddCell("" + PromTriGenFin);
            table.AddCell("" + NivelGenFin);

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
            table.AddCell("" + CalifDici[10]);
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
            table.AddCell("" + SumaInasisTri1);
            table.AddCell(" ");
            table.AddCell("" + SumaInasisTri2);
            table.AddCell(" ");
            table.AddCell("" + SumaInasisTri3);
            table.AddCell(" ");
            table.AddCell("" + SumInasisTriFin);
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
                        if (cmbsepina.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. ¡No se puede Editar!");
                        }
                        else
                        {
                            if (cmbdiaginasis.Enabled == false)
                            {
                                if (ValidaCampos(BoxGenero) == true)
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
                        if (cmbOctubreIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbOctubreIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox1) == true)
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
                            if (cmbOctubreIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox2) == true)
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
                            if (cmbNovIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox3) == true)
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
                                if (ValidaCampos(groupBox4) == true)
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
                        if (cmbfebreroIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbEneroIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox5) == true)
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
                        if (cmbmarzoina.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbfebreroIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox6) == true)
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
                            if (cmbmarzoina.Enabled == false)
                            {
                                if (ValidaCampos(groupBox7) == true)
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
                        if (cmbJunioinasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbMayoIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox9) == true)
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
                        if (cmbdiaginasis.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (ValidaCampos(groupBox10) == true)
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
        

    private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            //-------------Ingresar los datos del alumno en pdf--------------------------------
            MySqlConnection conn112;
            MySqlCommand com112;

            string conexion112 = "server=localhost;uid=root;database=nerivela";
            string query112 = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            string nombre112, Apellidop112, Apellidom112, IdAlumno112;

            conn112 = new MySqlConnection(conexion112);
            conn112.Open();

            com112 = new MySqlCommand(query112, conn112);

            MySqlDataReader myreader112 = com112.ExecuteReader();

            myreader112.Read();
            nombre112 = Convert.ToString(myreader112["nombre"]);
            Apellidop112 = Convert.ToString(myreader112["ApellidoP"]);
            Apellidom112 = Convert.ToString(myreader112["ApellidoM"]);
            sesion.grado = Convert.ToString(myreader112["idGrado"]);
            IdAlumno112 = Convert.ToString(myreader112["idAlumno"]);
            conn112.Close();

            if (Convert.ToInt32(sesion.grado) == 4)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
                string nombre1, Apellidop1, Apellidom1, IdAlumno1;

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                myreader.Read();
                nombre1 = Convert.ToString(myreader["nombre"]);
                Apellidop1 = Convert.ToString(myreader["ApellidoP"]);
                Apellidom1 = Convert.ToString(myreader["ApellidoM"]);
                sesion.grado = Convert.ToString(myreader["idGrado"]);
                IdAlumno1 = Convert.ToString(myreader["idAlumno"]);
                conn.Close();

                //-------------------------------Ingresar las calificaciones mensuales de los alumnos---------------------

                //Septiembre------------------------------------------
                string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Septiembre'";

                MySqlConnection conn1;
                MySqlCommand com1;

                conn1 = new MySqlConnection(conexion);
                conn1.Open();

                com1 = new MySqlCommand(CalifSep, conn1);

                MySqlDataReader myreader1 = com1.ExecuteReader();

                string[] CalifSept = new string[11];
                string PromSept = " ";
                string PromSeptGeneral = " ";

                if (myreader1.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader1.Read())//Agrega calificaciones
                    {
                        double[] CalifSeptemp = new double[11];
                        CalifSeptemp[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                        CalifSept[L] = CalifSeptemp[L].ToString("0.#");
                        L++;
                    }

                    double PromSeptTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromSeptTemp = PromSeptTemp + Convert.ToDouble(CalifSept[i]);
                    }

                    PromSeptTemp = PromSeptTemp / 9;
                    PromSept = PromSeptTemp.ToString("0.#"); //Promedio de septiembre

                    double PromSeptGen = 0;

                    PromSeptGen = Convert.ToDouble(PromSept) + Convert.ToDouble(CalifSept[9]);
                    PromSeptGen = PromSeptGen / 2;
                    PromSeptGeneral = PromSeptGen.ToString("0.#"); //Promedio general de semptiembre
                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifSept[i] = " ";
                    }
                }
                conn1.Close();

                //Octubre-----------------------------------------------------------------
                string CalifOct = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Octubre'";

                MySqlConnection conn2;
                MySqlCommand com2;

                conn2 = new MySqlConnection(conexion);
                conn2.Open();

                com2 = new MySqlCommand(CalifOct, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] CalifOctu = new string[11];
                string PromOct = " ";
                string PromOctuGeneral = " ";

                if (myreader2.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader2.Read())//Agrega calificaciones
                    {
                        double[] CalifOctutemp = new double[11];
                        CalifOctutemp[L] = Convert.ToDouble(myreader2["CalificacionMen"]);
                        CalifOctu[L] = CalifOctutemp[L].ToString("0.#");
                        L++;
                    }

                    double PromOctTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromOctTemp = PromOctTemp + Convert.ToDouble(CalifOctu[i]);
                    }

                    PromOctTemp = PromOctTemp / 9;
                    PromOct = PromOctTemp.ToString("0.#"); //Promedio de octubre

                    double PromOctGen = 0;

                    PromOctGen = Convert.ToDouble(PromOct) + Convert.ToDouble(CalifOctu[9]);
                    PromOctGen = PromOctGen / 2;
                    PromOctuGeneral = PromOctGen.ToString("0.#"); //Promedio general de semptiembre
                }
                else
                {

                    for (int i = 0; i < 11; i++)
                    {
                        CalifOctu[i] = " ";
                    }
                }
                conn2.Close();
                //Noviembre-----------------------------------------------------------------
                string CalifNov = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Noviembre'";

                MySqlConnection conn3;
                MySqlCommand com3;

                conn3 = new MySqlConnection(conexion);
                conn3.Open();

                com3 = new MySqlCommand(CalifNov, conn3);

                MySqlDataReader myreader3 = com3.ExecuteReader();

                string[] CalifNovi = new string[11];
                string PromNov = " ";
                string PromTriSen1 = " ";
                string PromTriEsp1 = " ";
                string PromTriMat1 = " ";
                string PromTriIng1 = " ";
                string PromTricn1 = " ";
                string PromTriGeo1 = " ";
                string PromTriHis1 = " ";
                string PromTriCiv1 = " ";
                string PromTriArt1 = " ";
                string PromTriFis1 = " ";
                string PromTriSoc1 = " ";
                string SumaInasisTri1 = " ";
                string NivelPromSen = " ";
                string Nivel1 = " ";
                string NivelMat1 = " ";
                string NivelIng1 = " ";
                string Nivelcn1 = " ";
                string NivelGeo1 = " ";
                string NivelHis1 = " ";
                string NivelCiv1 = " ";
                string NivelArt1 = " ";
                string NivelFis1 = " ";
                string NivelSoc1 = " ";
                string PromNoviGeneral = " ";
                string PromTriGen1 = " ";
                string NivelGen1 = " ";

                string NivelPromDes = " ";
                string PromTriDes1 = " ";

                if (myreader3.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader3.Read())//Agrega calificaciones
                    {
                        double[] CalifNovitemp = new double[11];
                        CalifNovitemp[L] = Convert.ToDouble(myreader3["CalificacionMen"]);
                        CalifNovi[L] = CalifNovitemp[L].ToString("0.#");
                        L++;
                    }

                    double PromNovTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromNovTemp = PromNovTemp + Convert.ToDouble(CalifNovi[i]);
                    }

                    PromNovTemp = PromNovTemp / 9;
                    PromNov = PromNovTemp.ToString("0.#");//Promedio de noviembre

                    double PromNovGen = 0;

                    PromNovGen = Convert.ToDouble(PromNov) + Convert.ToDouble(CalifNovi[9]);
                    PromNovGen = PromNovGen / 2;
                    PromNoviGeneral = PromNovGen.ToString("0.#"); //Promedio general de nociembre

                    //------------------------------------Promedio de materias sencillas primer trimestre-------------------------------------------------
                    double PromSen31 = 0;

                    PromSen31 = Convert.ToDouble(PromSept) + Convert.ToDouble(PromOct) + Convert.ToDouble(PromNov);
                    PromSen31 = PromSen31 / 3;
                    PromTriSen1 = PromSen31.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                    {
                        NivelPromSen = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                        {
                            NivelPromSen = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                            {
                                NivelPromSen = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) == 10)
                                {
                                    NivelPromSen = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromSen = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Promedio General primer trimestre-------------------------------------------------
                    double PromGen31 = 0;

                    PromGen31 = Convert.ToDouble(PromSeptGeneral) + Convert.ToDouble(PromOctuGeneral) + Convert.ToDouble(PromNoviGeneral);
                    PromGen31 = PromGen31 / 3;
                    PromTriGen1 = PromGen31.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriGen1) >= 5 && Convert.ToDouble(PromTriGen1) < 6)
                    {
                        NivelGen1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen1) >= 6 && Convert.ToDouble(PromTriGen1) < 8)
                        {
                            NivelGen1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen1) >= 8 && Convert.ToDouble(PromTriGen1) < 10)
                            {
                                NivelGen1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen1) == 10)
                                {
                                    NivelGen1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGen1 = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Español-------------------------------------------------
                    double PromEsp31 = 0;

                    PromEsp31 = Convert.ToDouble(CalifSept[0]) + Convert.ToDouble(CalifOctu[0]) + Convert.ToDouble(CalifNovi[0]);
                    PromEsp31 = PromEsp31 / 3;
                    PromTriEsp1 = PromEsp31.ToString("0.#");//Promedio del primer trimestre de español

                    //Asignacion del nivel del primer trimestre de español
                    if (Convert.ToDouble(PromTriEsp1) >= 5 && Convert.ToDouble(PromTriEsp1) < 6)
                    {
                        Nivel1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp1) >= 6 && Convert.ToDouble(PromTriEsp1) < 8)
                        {
                            Nivel1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp1) >= 8 && Convert.ToDouble(PromTriEsp1) < 10)
                            {
                                Nivel1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp1) == 10)
                                {
                                    Nivel1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivel1 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Matematicas--------------------------------------------------------
                    double PromMat31 = 0;

                    PromMat31 = Convert.ToDouble(CalifSept[1]) + Convert.ToDouble(CalifOctu[1]) + Convert.ToDouble(CalifNovi[1]);
                    PromMat31 = PromMat31 / 3;
                    PromTriMat1 = PromMat31.ToString("0.#");//Promedio del primer trimestre de matematicas

                    //Asignacion del nivel del primer trimestre de matematicas
                    if (Convert.ToDouble(PromTriMat1) >= 5 && Convert.ToDouble(PromTriMat1) < 6)
                    {
                        NivelMat1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat1) >= 6 && Convert.ToDouble(PromTriMat1) < 8)
                        {
                            NivelMat1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat1) >= 8 && Convert.ToDouble(PromTriMat1) < 10)
                            {
                                NivelMat1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat1) == 10)
                                {
                                    NivelMat1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelMat1 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ingles-------------------------------------------------
                    double PromIng31 = 0;

                    PromIng31 = Convert.ToDouble(CalifSept[2]) + Convert.ToDouble(CalifOctu[2]) + Convert.ToDouble(CalifNovi[2]);
                    PromIng31 = PromIng31 / 3;
                    PromTriIng1 = PromIng31.ToString("0.#");//Promedio del primer trimestre de Ingles

                    //Asignacion del nivel del primer trimestre de Ingles
                    if (Convert.ToDouble(PromTriIng1) >= 5 && Convert.ToDouble(PromTriIng1) < 6)
                    {
                        NivelIng1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng1) >= 6 && Convert.ToDouble(PromTriIng1) < 8)
                        {
                            NivelIng1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng1) >= 8 && Convert.ToDouble(PromTriIng1) < 10)
                            {
                                NivelIng1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng1) == 10)
                                {
                                    NivelIng1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelIng1 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                    double Promcn31 = 0;

                    Promcn31 = Convert.ToDouble(CalifSept[3]) + Convert.ToDouble(CalifOctu[3]) + Convert.ToDouble(CalifNovi[3]);
                    Promcn31 = Promcn31 / 3;
                    PromTricn1 = Promcn31.ToString("0.#");//Promedio del primer trimestre de Ciencias

                    //Asignacion del nivel del primer trimestre de Ciencias
                    if (Convert.ToDouble(PromTricn1) >= 5 && Convert.ToDouble(PromTricn1) < 6)
                    {
                        Nivelcn1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn1) >= 6 && Convert.ToDouble(PromTricn1) < 8)
                        {
                            Nivelcn1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn1) >= 8 && Convert.ToDouble(PromTricn1) < 10)
                            {
                                Nivelcn1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn1) == 10)
                                {
                                    Nivelcn1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivelcn1 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Geografia-------------------------------------------------
                    double PromGeo31 = 0;

                    PromGeo31 = Convert.ToDouble(CalifSept[4]) + Convert.ToDouble(CalifOctu[4]) + Convert.ToDouble(CalifNovi[4]);
                    PromGeo31 = PromGeo31 / 3;
                    PromTriGeo1 = PromGeo31.ToString("0.#");//Promedio del primer trimestre de Geografia

                    //Asignacion del nivel del primer trimestre de Geografia
                    if (Convert.ToDouble(PromTriGeo1) >= 5 && Convert.ToDouble(PromTriGeo1) < 6)
                    {
                        NivelGeo1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo1) >= 6 && Convert.ToDouble(PromTriGeo1) < 8)
                        {
                            NivelGeo1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo1) >= 8 && Convert.ToDouble(PromTriGeo1) < 10)
                            {
                                NivelGeo1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo1) == 10)
                                {
                                    NivelGeo1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGeo1 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Historia--------------------------------------------------------
                    double PromHis31 = 0;

                    PromHis31 = Convert.ToDouble(CalifSept[5]) + Convert.ToDouble(CalifOctu[5]) + Convert.ToDouble(CalifNovi[5]);
                    PromHis31 = PromHis31 / 3;
                    PromTriHis1 = PromHis31.ToString("0.#");//Promedio del primer trimestre de Historia

                    //Asignacion del nivel del primer trimestre de Historia
                    if (Convert.ToDouble(PromTriHis1) >= 5 && Convert.ToDouble(PromTriHis1) < 6)
                    {
                        NivelHis1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis1) >= 6 && Convert.ToDouble(PromTriHis1) < 8)
                        {
                            NivelHis1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis1) >= 8 && Convert.ToDouble(PromTriHis1) < 10)
                            {
                                NivelHis1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis1) == 10)
                                {
                                    NivelHis1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelHis1 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Formacion Civica y etica-------------------------------------------------
                    double PromCiv31 = 0;

                    PromCiv31 = Convert.ToDouble(CalifSept[6]) + Convert.ToDouble(CalifOctu[6]) + Convert.ToDouble(CalifNovi[6]);
                    PromCiv31 = PromCiv31 / 3;
                    PromTriCiv1 = PromCiv31.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                    //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                    if (Convert.ToDouble(PromTriCiv1) >= 5 && Convert.ToDouble(PromTriCiv1) < 6)
                    {
                        NivelCiv1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv1) >= 6 && Convert.ToDouble(PromTriCiv1) < 8)
                        {
                            NivelCiv1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv1) >= 8 && Convert.ToDouble(PromTriCiv1) < 10)
                            {
                                NivelCiv1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv1) == 10)
                                {
                                    NivelCiv1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelCiv1 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Artes--------------------------------------------------------
                    double PromArt31 = 0;

                    PromArt31 = Convert.ToDouble(CalifSept[7]) + Convert.ToDouble(CalifOctu[7]) + Convert.ToDouble(CalifNovi[7]);
                    PromArt31 = PromArt31 / 3;
                    PromTriArt1 = PromArt31.ToString("0.#");//Promedio del primer trimestre de Artes

                    //Asignacion del nivel del primer trimestre de Artes
                    if (Convert.ToDouble(PromTriArt1) >= 5 && Convert.ToDouble(PromTriArt1) < 6)
                    {
                        NivelArt1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt1) >= 6 && Convert.ToDouble(PromTriArt1) < 8)
                        {
                            NivelArt1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt1) >= 8 && Convert.ToDouble(PromTriArt1) < 10)
                            {
                                NivelArt1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt1) == 10)
                                {
                                    NivelArt1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelArt1 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ed. Fisica-------------------------------------------------
                    double PromFis31 = 0;

                    PromFis31 = Convert.ToDouble(CalifSept[8]) + Convert.ToDouble(CalifOctu[8]) + Convert.ToDouble(CalifNovi[8]);
                    PromFis31 = PromFis31 / 3;
                    PromTriFis1 = PromFis31.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                    //Asignacion del nivel del primer trimestre de Ed. Fisica
                    if (Convert.ToDouble(PromTriFis1) >= 5 && Convert.ToDouble(PromTriFis1) < 6)
                    {
                        NivelFis1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis1) >= 6 && Convert.ToDouble(PromTriFis1) < 8)
                        {
                            NivelFis1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis1) >= 8 && Convert.ToDouble(PromTriFis1) < 10)
                            {
                                NivelFis1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis1) == 10)
                                {
                                    NivelFis1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelFis1 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                    double PromSoc31 = 0;

                    PromSoc31 = Convert.ToDouble(CalifSept[9]) + Convert.ToDouble(CalifOctu[9]) + Convert.ToDouble(CalifNovi[9]);
                    PromSoc31 = PromSoc31 / 3;
                    PromTriSoc1 = PromSoc31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                    //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                    if (Convert.ToDouble(PromTriSoc1) >= 5 && Convert.ToDouble(PromTriSoc1) < 6)
                    {
                        NivelSoc1 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc1) >= 6 && Convert.ToDouble(PromTriSoc1) < 8)
                        {
                            NivelSoc1 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc1) >= 8 && Convert.ToDouble(PromTriSoc1) < 10)
                            {
                                NivelSoc1 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc1) == 10)
                                {
                                    NivelSoc1 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelSoc1 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Promedio de materias desarrollo social primer trimestre-------------------------------------------------
                    double PromDes31 = 0;

                    PromDes31 = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriSoc1);
                    PromDes31 = PromDes31 / 3;
                    PromTriDes1 = PromDes31.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriDes1) >= 5 && Convert.ToDouble(PromTriDes1) < 6)
                    {
                        NivelPromDes = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriDes1) >= 6 && Convert.ToDouble(PromTriDes1) < 8)
                        {
                            NivelPromDes = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes1) >= 8 && Convert.ToDouble(PromTriDes1) < 10)
                            {
                                NivelPromDes = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes1) == 10)
                                {
                                    NivelPromDes = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromDes = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Inasistencias----------------------------------------------------
                    double SumaInasis31 = 0;

                    SumaInasis31 = Convert.ToDouble(CalifSept[10]) + Convert.ToDouble(CalifOctu[10]) + Convert.ToDouble(CalifNovi[10]);
                    SumaInasisTri1 = SumaInasis31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                }
                else
                {

                    for (int i = 0; i < 11; i++)
                    {
                        CalifNovi[i] = " ";
                    }
                }
                conn3.Close();

                //Diciembre-----------------------------------------------------------------
                string CalifDic = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diciembre'";

                MySqlConnection conn4;
                MySqlCommand com4;

                conn4 = new MySqlConnection(conexion);
                conn4.Open();

                com4 = new MySqlCommand(CalifDic, conn4);

                MySqlDataReader myreader4 = com4.ExecuteReader();

                string[] CalifDici = new string[11];
                string PromDic = " ";
                string PromDiciGeneral = " ";

                if (myreader4.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader4.Read())//Agrega calificaciones
                    {
                        double[] CalifDicitemp = new double[11];
                        CalifDicitemp[L] = Convert.ToDouble(myreader4["CalificacionMen"]);
                        CalifDici[L] = CalifDicitemp[L].ToString("0.#");
                        L++;
                    }

                    double PromDicTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromDicTemp = PromDicTemp + Convert.ToDouble(CalifDici[i]);
                    }

                    PromDicTemp = PromDicTemp / 9;
                    PromDic = PromDicTemp.ToString("0.#");//Promedio de diciembre

                    double PromDiciGen = 0;

                    PromDiciGen = Convert.ToDouble(PromDic) + Convert.ToDouble(CalifDici[9]);
                    PromDiciGen = PromDiciGen / 2;
                    PromDiciGeneral = PromDiciGen.ToString("0.#"); //Promedio general de diciembre
                }
                else
                {

                    for (int i = 0; i < 11; i++)
                    {
                        CalifDici[i] = " ";
                    }
                }
                conn4.Close();
                //Enero-----------------------------------------------------------------
                string CalifEne = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Enero'";

                MySqlConnection conn5;
                MySqlCommand com5;

                conn5 = new MySqlConnection(conexion);
                conn5.Open();

                com5 = new MySqlCommand(CalifEne, conn5);

                MySqlDataReader myreader5 = com5.ExecuteReader();

                string[] CalifEner = new string[11];
                string PromEne = " ";
                string PromEnerGeneral = " ";

                if (myreader5.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader5.Read())//Agrega calificaciones
                    {
                        double[] CalifEnertemp = new double[11];
                        CalifEnertemp[L] = Convert.ToDouble(myreader5["CalificacionMen"]);
                        CalifEner[L] = CalifEnertemp[L].ToString("0.#");
                        L++;
                    }

                    double PromEneTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromEneTemp = PromEneTemp + Convert.ToDouble(CalifEner[i]);
                    }

                    PromEneTemp = PromEneTemp / 9;
                    PromEne = PromEneTemp.ToString("0.#");//Promedio de enero

                    double PromEnerGen = 0;

                    PromEnerGen = Convert.ToDouble(PromEne) + Convert.ToDouble(CalifEner[9]);
                    PromEnerGen = PromEnerGen / 2;
                    PromEnerGeneral = PromEnerGen.ToString("0.#"); //Promedio general de enero
                }
                else
                {

                    for (int i = 0; i < 11; i++)
                    {
                        CalifEner[i] = " ";
                    }
                }
                conn5.Close();

                //Febrero-----------------------------------------------------------------
                string CalifFeb = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Febrero'";

                MySqlConnection conn6;
                MySqlCommand com6;

                conn6 = new MySqlConnection(conexion);
                conn6.Open();

                com6 = new MySqlCommand(CalifFeb, conn6);

                MySqlDataReader myreader6 = com6.ExecuteReader();

                string[] CalifFebr = new string[11];
                string PromFeb = " ";
                string PromTriEsp2 = " ";
                string PromTriMat2 = " ";
                string PromTriIng2 = " ";
                string PromTricn2 = " ";
                string PromTriGeo2 = " ";
                string PromTriHis2 = " ";
                string PromTriCiv2 = " ";
                string PromTriArt2 = " ";
                string PromTriFis2 = " ";
                string PromTriSoc2 = " ";
                string PromTriSen2 = " ";
                string SumaInasisTri2 = " ";
                string Nivel2 = " ";
                string NivelMat2 = " ";
                string NivelIng2 = " ";
                string Nivelcn2 = " ";
                string NivelGeo2 = " ";
                string NivelHis2 = " ";
                string NivelCiv2 = " ";
                string NivelArt2 = " ";
                string NivelFis2 = " ";
                string NivelSoc2 = " ";
                string NivelPromSen2 = " ";
                string PromFebrGeneral = " ";
                string PromTriGen2 = " ";
                string NivelGen2 = " ";

                string NivelPromDes2 = " ";
                string PromTriDes2 = " ";

                if (myreader6.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader6.Read())//Agrega calificaciones
                    {
                        double[] CalifFebrtemp = new double[11];
                        CalifFebrtemp[L] = Convert.ToDouble(myreader6["CalificacionMen"]);
                        CalifFebr[L] = CalifFebrtemp[L].ToString("0.#");
                        L++;
                    }

                    double PromFebTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromFebTemp = PromFebTemp + Convert.ToDouble(CalifFebr[i]);
                    }

                    PromFebTemp = PromFebTemp / 9;
                    PromFeb = PromFebTemp.ToString("0.#");//Promedio de febrero

                    double PromFebrGen = 0;

                    PromFebrGen = Convert.ToDouble(PromFeb) + Convert.ToDouble(CalifFebr[9]);
                    PromFebrGen = PromFebrGen / 2;
                    PromFebrGeneral = PromFebrGen.ToString("0.#"); //Promedio general de febrero

                    //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                    double PromSen32 = 0;

                    PromSen32 = Convert.ToDouble(PromEne) + Convert.ToDouble(PromDic) + Convert.ToDouble(PromFeb);
                    PromSen32 = PromSen32 / 3;
                    PromTriSen2 = PromSen32.ToString("0.#");//Promedio del segundo trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                    {
                        NivelPromSen2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                        {
                            NivelPromSen2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                            {
                                NivelPromSen2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) == 10)
                                {
                                    NivelPromSen2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromSen2 = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Promedio General segundo trimestre-------------------------------------------------
                    double PromGen32 = 0;

                    PromGen32 = Convert.ToDouble(PromDiciGeneral) + Convert.ToDouble(PromEnerGeneral) + Convert.ToDouble(PromFebrGeneral);
                    PromGen32 = PromGen32 / 3;
                    PromTriGen2 = PromGen32.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriGen2) >= 5 && Convert.ToDouble(PromTriGen2) < 6)
                    {
                        NivelGen2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen2) >= 6 && Convert.ToDouble(PromTriGen2) < 8)
                        {
                            NivelGen2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen2) >= 8 && Convert.ToDouble(PromTriGen2) < 10)
                            {
                                NivelGen2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen2) == 10)
                                {
                                    NivelGen2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGen2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Español-------------------------------------------------
                    double PromEsp32 = 0;

                    PromEsp32 = Convert.ToDouble(CalifDici[0]) + Convert.ToDouble(CalifEner[0]) + Convert.ToDouble(CalifFebr[0]);
                    PromEsp32 = PromEsp32 / 3;
                    PromTriEsp2 = PromEsp32.ToString("0.#");//Promedio del primer trimestre de español

                    //Asignacion del nivel del primer trimestre de español
                    if (Convert.ToDouble(PromTriEsp2) >= 5 && Convert.ToDouble(PromTriEsp2) < 6)
                    {
                        Nivel2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp2) >= 6 && Convert.ToDouble(PromTriEsp2) < 8)
                        {
                            Nivel2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp2) >= 8 && Convert.ToDouble(PromTriEsp2) < 10)
                            {
                                Nivel2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp2) == 10)
                                {
                                    Nivel2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivel2 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Matematicas--------------------------------------------------------
                    double PromMat32 = 0;

                    PromMat32 = Convert.ToDouble(CalifDici[1]) + Convert.ToDouble(CalifEner[1]) + Convert.ToDouble(CalifFebr[1]);
                    PromMat32 = PromMat32 / 3;
                    PromTriMat2 = PromMat32.ToString("0.#");//Promedio del primer trimestre de matematicas

                    //Asignacion del nivel del primer trimestre de matematicas
                    if (Convert.ToDouble(PromTriMat2) >= 5 && Convert.ToDouble(PromTriMat2) < 6)
                    {
                        NivelMat2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat2) >= 6 && Convert.ToDouble(PromTriMat2) < 8)
                        {
                            NivelMat2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat2) >= 8 && Convert.ToDouble(PromTriMat2) < 10)
                            {
                                NivelMat2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat2) == 10)
                                {
                                    NivelMat2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelMat2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ingles-------------------------------------------------
                    double PromIng32 = 0;

                    PromIng32 = Convert.ToDouble(CalifDici[2]) + Convert.ToDouble(CalifEner[2]) + Convert.ToDouble(CalifFebr[2]);
                    PromIng32 = PromIng32 / 3;
                    PromTriIng2 = PromIng32.ToString("0.#");//Promedio del primer trimestre de Ingles

                    //Asignacion del nivel del primer trimestre de Ingles
                    if (Convert.ToDouble(PromTriIng2) >= 5 && Convert.ToDouble(PromTriIng2) < 6)
                    {
                        NivelIng2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng2) >= 6 && Convert.ToDouble(PromTriIng2) < 8)
                        {
                            NivelIng2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng2) >= 8 && Convert.ToDouble(PromTriIng2) < 10)
                            {
                                NivelIng2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng2) == 10)
                                {
                                    NivelIng2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelIng2 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                    double Promcn32 = 0;

                    Promcn32 = Convert.ToDouble(CalifDici[3]) + Convert.ToDouble(CalifEner[3]) + Convert.ToDouble(CalifFebr[3]);
                    Promcn32 = Promcn32 / 3;
                    PromTricn2 = Promcn32.ToString("0.#");//Promedio del primer trimestre de Ciencias

                    //Asignacion del nivel del primer trimestre de Ciencias
                    if (Convert.ToDouble(PromTricn2) >= 5 && Convert.ToDouble(PromTricn2) < 6)
                    {
                        Nivelcn2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn2) >= 6 && Convert.ToDouble(PromTricn2) < 8)
                        {
                            Nivelcn2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn2) >= 8 && Convert.ToDouble(PromTricn2) < 10)
                            {
                                Nivelcn2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn2) == 10)
                                {
                                    Nivelcn2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivelcn2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Geografia-------------------------------------------------
                    double PromGeo32 = 0;

                    PromGeo32 = Convert.ToDouble(CalifDici[4]) + Convert.ToDouble(CalifEner[4]) + Convert.ToDouble(CalifFebr[4]);
                    PromGeo32 = PromGeo32 / 3;
                    PromTriGeo2 = PromGeo32.ToString("0.#");//Promedio del primer trimestre de Geografia

                    //Asignacion del nivel del primer trimestre de Geografia
                    if (Convert.ToDouble(PromTriGeo2) >= 5 && Convert.ToDouble(PromTriGeo2) < 6)
                    {
                        NivelGeo2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo2) >= 6 && Convert.ToDouble(PromTriGeo2) < 8)
                        {
                            NivelGeo2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo2) >= 8 && Convert.ToDouble(PromTriGeo2) < 10)
                            {
                                NivelGeo2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo2) == 10)
                                {
                                    NivelGeo2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGeo2 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Historia--------------------------------------------------------
                    double PromHis32 = 0;

                    PromHis32 = Convert.ToDouble(CalifDici[5]) + Convert.ToDouble(CalifEner[5]) + Convert.ToDouble(CalifFebr[5]);
                    PromHis32 = PromHis32 / 3;
                    PromTriHis2 = PromHis32.ToString("0.#");//Promedio del primer trimestre de Historia

                    //Asignacion del nivel del primer trimestre de Historia
                    if (Convert.ToDouble(PromTriHis2) >= 5 && Convert.ToDouble(PromTriHis2) < 6)
                    {
                        NivelHis2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis2) >= 6 && Convert.ToDouble(PromTriHis2) < 8)
                        {
                            NivelHis2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis2) >= 8 && Convert.ToDouble(PromTriHis2) < 10)
                            {
                                NivelHis2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis2) == 10)
                                {
                                    NivelHis2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelHis2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Formacion Civica y etica-------------------------------------------------
                    double PromCiv32 = 0;

                    PromCiv32 = Convert.ToDouble(CalifDici[6]) + Convert.ToDouble(CalifEner[6]) + Convert.ToDouble(CalifFebr[6]);
                    PromCiv32 = PromCiv32 / 3;
                    PromTriCiv2 = PromCiv32.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                    //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                    if (Convert.ToDouble(PromTriCiv2) >= 5 && Convert.ToDouble(PromTriCiv2) < 6)
                    {
                        NivelCiv2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv2) >= 6 && Convert.ToDouble(PromTriCiv2) < 8)
                        {
                            NivelCiv2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv2) >= 8 && Convert.ToDouble(PromTriCiv2) < 10)
                            {
                                NivelCiv2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv2) == 10)
                                {
                                    NivelCiv2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelCiv2 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Artes--------------------------------------------------------
                    double PromArt32 = 0;

                    PromArt32 = Convert.ToDouble(CalifDici[7]) + Convert.ToDouble(CalifEner[7]) + Convert.ToDouble(CalifFebr[7]);
                    PromArt32 = PromArt32 / 3;
                    PromTriArt2 = PromArt32.ToString("0.#");//Promedio del primer trimestre de Artes

                    //Asignacion del nivel del primer trimestre de Artes
                    if (Convert.ToDouble(PromTriArt2) >= 5 && Convert.ToDouble(PromTriArt2) < 6)
                    {
                        NivelArt2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt2) >= 6 && Convert.ToDouble(PromTriArt2) < 8)
                        {
                            NivelArt2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt2) >= 8 && Convert.ToDouble(PromTriArt2) < 10)
                            {
                                NivelArt2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt2) == 10)
                                {
                                    NivelArt2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelArt2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ed. Fisica-------------------------------------------------
                    double PromFis32 = 0;

                    PromFis32 = Convert.ToDouble(CalifDici[8]) + Convert.ToDouble(CalifEner[8]) + Convert.ToDouble(CalifFebr[8]);
                    PromFis32 = PromFis32 / 3;
                    PromTriFis2 = PromFis32.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                    //Asignacion del nivel del primer trimestre de Ed. Fisica
                    if (Convert.ToDouble(PromTriFis2) >= 5 && Convert.ToDouble(PromTriFis2) < 6)
                    {
                        NivelFis2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis2) >= 6 && Convert.ToDouble(PromTriFis2) < 8)
                        {
                            NivelFis2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis2) >= 8 && Convert.ToDouble(PromTriFis2) < 10)
                            {
                                NivelFis2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis2) == 10)
                                {
                                    NivelFis2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelFis2 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                    double PromSoc32 = 0;

                    PromSoc32 = Convert.ToDouble(CalifDici[9]) + Convert.ToDouble(CalifEner[9]) + Convert.ToDouble(CalifFebr[9]);
                    PromSoc32 = PromSoc32 / 3;
                    PromTriSoc2 = PromSoc32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                    //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                    if (Convert.ToDouble(PromTriSoc2) >= 5 && Convert.ToDouble(PromTriSoc2) < 6)
                    {
                        NivelSoc2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc2) >= 6 && Convert.ToDouble(PromTriSoc2) < 8)
                        {
                            NivelSoc2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc2) >= 8 && Convert.ToDouble(PromTriSoc2) < 10)
                            {
                                NivelSoc2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc2) == 10)
                                {
                                    NivelSoc2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelSoc2 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Promedio de materias desarrollo social segundo trimestre-------------------------------------------------
                    double PromDes32 = 0;

                    PromDes32 = Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriSoc2);
                    PromDes32 = PromDes32 / 3;
                    PromTriDes2 = PromDes32.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriDes2) >= 5 && Convert.ToDouble(PromTriDes2) < 6)
                    {
                        NivelPromDes2 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriDes2) >= 6 && Convert.ToDouble(PromTriDes2) < 8)
                        {
                            NivelPromDes2 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes2) >= 8 && Convert.ToDouble(PromTriDes2) < 10)
                            {
                                NivelPromDes2 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes2) == 10)
                                {
                                    NivelPromDes2 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromDes2 = " ";
                                }
                            }
                        }
                    }

                    //-----------------------------Inasistencia-----------------------------------------------------
                    double SumaInasis32 = 0;

                    SumaInasis32 = Convert.ToDouble(CalifDici[10]) + Convert.ToDouble(CalifEner[10]) + Convert.ToDouble(CalifFebr[10]);
                    SumaInasisTri2 = SumaInasis32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifFebr[i] = " ";
                    }
                }
                conn6.Close();
                //Marzo-----------------------------------------------------------------
                string CalifMar = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Marzo'";

                MySqlConnection conn7;
                MySqlCommand com7;

                conn7 = new MySqlConnection(conexion);
                conn7.Open();

                com7 = new MySqlCommand(CalifMar, conn7);

                MySqlDataReader myreader7 = com7.ExecuteReader();

                string[] CalifMarz = new string[11];
                string PromMar = " ";
                string PromMarzGeneral = " ";

                if (myreader7.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader7.Read())//Agrega calificaciones
                    {
                        double[] CalifMarztemp = new double[11];
                        CalifMarztemp[L] = Convert.ToDouble(myreader7["CalificacionMen"]);
                        CalifMarz[L] = CalifMarztemp[L].ToString("0.#");
                        L++;
                    }

                    double PromMarTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromMarTemp = PromMarTemp + Convert.ToDouble(CalifMarz[i]);
                    }

                    PromMarTemp = PromMarTemp / 9;
                    PromMar = PromMarTemp.ToString("0.#");//Promedio de  marzo

                    double PromMarzGen = 0;

                    PromMarzGen = Convert.ToDouble(PromMar) + Convert.ToDouble(CalifMarz[9]);
                    PromMarzGen = PromMarzGen / 2;
                    PromMarzGeneral = PromMarzGen.ToString("0.#"); //Promedio general de marzo

                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifMarz[i] = " ";
                    }
                }
                conn7.Close();
                //Abril-----------------------------------------------------------------
                string CalifAbr = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Abril'";

                MySqlConnection conn8;
                MySqlCommand com8;

                conn8 = new MySqlConnection(conexion);
                conn8.Open();

                com8 = new MySqlCommand(CalifAbr, conn8);

                MySqlDataReader myreader8 = com8.ExecuteReader();

                string[] CalifAbri = new string[11];
                string PromAbr = " ";
                string PromAbriGeneral = " ";

                if (myreader8.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader8.Read())//Agrega calificaciones
                    {
                        double[] CalifAbritemp = new double[11];
                        CalifAbritemp[L] = Convert.ToDouble(myreader8["CalificacionMen"]);
                        CalifAbri[L] = CalifAbritemp[L].ToString("0.#");
                        L++;
                    }

                    double PromAbrTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromAbrTemp = PromAbrTemp + Convert.ToDouble(CalifAbri[i]);
                    }

                    PromAbrTemp = PromAbrTemp / 9;
                    PromAbr = PromAbrTemp.ToString("0.#");//Promedio de abril

                    double PromAbriGen = 0;

                    PromAbriGen = Convert.ToDouble(PromAbr) + Convert.ToDouble(CalifAbri[9]);
                    PromAbriGen = PromAbriGen / 2;
                    PromAbriGeneral = PromAbriGen.ToString("0.#"); //Promedio general de abril

                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifAbri[i] = " ";
                    }
                }
                conn8.Close();
                //Mayo-----------------------------------------------------------------
                string CalifMay = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Mayo'";

                MySqlConnection conn9;
                MySqlCommand com9;

                conn9 = new MySqlConnection(conexion);
                conn9.Open();

                com9 = new MySqlCommand(CalifMay, conn9);

                MySqlDataReader myreader9 = com9.ExecuteReader();

                string[] CalifMayo = new string[11];
                string PromMay = " ";
                string PromMayoGeneral = " ";

                if (myreader9.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader9.Read())//Agrega calificaciones
                    {
                        double[] CalifMayotemp = new double[11];
                        CalifMayotemp[L] = Convert.ToDouble(myreader9["CalificacionMen"]);
                        CalifMayo[L] = CalifMayotemp[L].ToString("0.#");
                        L++;
                    }

                    double PromMayTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromMayTemp = PromMayTemp + Convert.ToDouble(CalifMayo[i]);
                    }

                    PromMayTemp = PromMayTemp / 9;
                    PromMay = PromMayTemp.ToString("0.#");//Promedio de mayo

                    double PromMayoGen = 0;

                    PromMayoGen = Convert.ToDouble(PromMay) + Convert.ToDouble(CalifMayo[9]);
                    PromMayoGen = PromMayoGen / 2;
                    PromMayoGeneral = PromMayoGen.ToString("0.#"); //Promedio general de mayo
                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifMayo[i] = " ";
                    }
                }
                conn9.Close();
                //Junio-----------------------------------------------------------------
                string CalifJun = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Junio'";

                MySqlConnection conn10;
                MySqlCommand com10;

                conn10 = new MySqlConnection(conexion);
                conn10.Open();

                com10 = new MySqlCommand(CalifJun, conn10);

                MySqlDataReader myreader10 = com10.ExecuteReader();

                string[] CalifJuni = new string[11];
                string PromJun = " ";
                string PromTriEsp3 = " ";
                string PromTriMat3 = " ";
                string PromTriIng3 = " ";
                string PromTricn3 = " ";
                string PromTriGeo3 = " ";
                string PromTriHis3 = " ";
                string PromTriCiv3 = " ";
                string PromTriArt3 = " ";
                string PromTriFis3 = " ";
                string PromTriSoc3 = " ";
                string SumaInasisTri3 = " ";
                string Nivel3 = " ";
                string NivelMat3 = " ";
                string NivelIng3 = " ";
                string Nivelcn3 = " ";
                string NivelGeo3 = " ";
                string NivelHis3 = " ";
                string NivelCiv3 = " ";
                string NivelArt3 = " ";
                string NivelFis3 = " ";
                string NivelSoc3 = " ";
                string PromTriGen3 = " ";
                string NivelGen3 = " ";
                string PromFinTriEsp = " ";
                string NivelFin = " ";
                string PromFinTriMat = " ";
                string NivelMatFin = " ";
                string PromFinTriIng = " ";
                string NivelIngFin = " ";
                string PromFinTricn = " ";
                string NivelcnFin = " ";
                string PromFinTriGeo = " ";
                string NivelGeoFin = " ";
                string PromFinTriHis = " ";
                string NivelHisFin = " ";
                string PromFinTriCiv = " ";
                string NivelCivFin = " ";
                string PromFinTriArt = " ";
                string NivelArtFin = " ";
                string PromFinTriFis = " ";
                string NivelFisFin = " ";
                string PromFinTriSoc = " ";
                string NivelSocFin = " ";
                string PromTriSen3 = " ";
                string NivelPromSen3 = " ";
                string PromTriSenFin = " ";
                string NivelPromSenFin = " ";
                string PromTriGenFin = " ";
                string NivelGenFin = " ";
                string SumInasisTriFin = " ";

                string PromTriDes3 = " ";
                string NivelPromDes3 = " ";
                string PromTriDesFin = " ";
                string NivelPromDesFin = " ";
                string PorcAsistencias = " ";

                string Si = " ";
                string No = " ";
                string Yes = " ";
                string Nou = " ";
                string Yes1 = " ";
                string Nou1 = " ";
                string Yes2 = " ";
                string Nou2 = " ";
                string Yes3 = " ";
                string Nou3 = " ";
                string Yes4 = " ";
                string Nou4 = " ";
                string PromMateriasIC = " ";
                string SumAsisTriFin = " ";

                string PromJuniGeneral = " ";

                if (myreader10.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader10.Read())//Agrega calificaciones
                    {
                        double[] CalifJunitemp = new double[11];
                        CalifJunitemp[L] = Convert.ToDouble(myreader10["CalificacionMen"]);
                        CalifJuni[L] = CalifJunitemp[L].ToString("0.#");
                        L++;
                    }

                    double PromJunTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromJunTemp = PromJunTemp + Convert.ToDouble(CalifJuni[i]);
                    }

                    PromJunTemp = PromJunTemp / 9;
                    PromJun = PromJunTemp.ToString("0.#");//Promedio de junio

                    double PromJuniGen = 0;

                    PromJuniGen = Convert.ToDouble(PromJun) + Convert.ToDouble(CalifJuni[9]);
                    PromJuniGen = PromJuniGen / 2;
                    PromJuniGeneral = PromJuniGen.ToString("0.#"); //Promedio general de junio

                    //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                    double PromSen33 = 0;

                    PromSen33 = Convert.ToDouble(PromMar) + Convert.ToDouble(PromAbr) + Convert.ToDouble(PromMay) + Convert.ToDouble(PromJun);
                    PromSen33 = PromSen33 / 4;
                    PromTriSen3 = PromSen33.ToString("0.#");//Promedio del segundo trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriSen3) >= 5 && Convert.ToDouble(PromTriSen3) < 6)
                    {
                        NivelPromSen3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSen3) >= 6 && Convert.ToDouble(PromTriSen3) < 8)
                        {
                            NivelPromSen3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen3) >= 8 && Convert.ToDouble(PromTriSen3) < 10)
                            {
                                NivelPromSen3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen3) == 10)
                                {
                                    NivelPromSen3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromSen3 = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                    double PromGen33 = 0;

                    PromGen33 = Convert.ToDouble(PromMarzGeneral) + Convert.ToDouble(PromAbriGeneral) + Convert.ToDouble(PromMayoGeneral) + Convert.ToDouble(PromJuniGeneral);
                    PromGen33 = PromGen33 / 4;
                    PromTriGen3 = PromGen33.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriGen3) >= 5 && Convert.ToDouble(PromTriGen3) < 6)
                    {
                        NivelGen3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen3) >= 6 && Convert.ToDouble(PromTriGen3) < 8)
                        {
                            NivelGen3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen3) >= 8 && Convert.ToDouble(PromTriGen3) < 10)
                            {
                                NivelGen3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen3) == 10)
                                {
                                    NivelGen3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGen3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Español-------------------------------------------------

                    double PromEsp33 = 0;

                    PromEsp33 = Convert.ToDouble(CalifMarz[0]) + Convert.ToDouble(CalifAbri[0]) + Convert.ToDouble(CalifMayo[0]) + Convert.ToDouble(CalifJuni[0]);
                    PromEsp33 = PromEsp33 / 4;
                    PromTriEsp3 = PromEsp33.ToString("0.#");//Promedio del tercer trimestre de español

                    //Asignacion del nivel del tercer trimestre de español
                    if (Convert.ToDouble(PromTriEsp3) >= 5 && Convert.ToDouble(PromTriEsp3) < 6)
                    {
                        Nivel3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriEsp3) >= 6 && Convert.ToDouble(PromTriEsp3) < 8)
                        {
                            Nivel3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp3) >= 8 && Convert.ToDouble(PromTriEsp3) < 10)
                            {
                                Nivel3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp3) == 10)
                                {
                                    Nivel3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivel3 = " ";
                                }
                            }
                        }
                    }

                    //-------------------------------------Matematicas--------------------------------------------------------
                    double PromMat33 = 0;

                    PromMat33 = Convert.ToDouble(CalifMarz[1]) + Convert.ToDouble(CalifAbri[1]) + Convert.ToDouble(CalifMayo[1]) + Convert.ToDouble(CalifJuni[1]);
                    PromMat33 = PromMat33 / 4;
                    PromTriMat3 = PromMat33.ToString("0.#");//Promedio del primer trimestre de matematicas

                    //Asignacion del nivel del primer trimestre de matematicas
                    if (Convert.ToDouble(PromTriMat3) >= 5 && Convert.ToDouble(PromTriMat3) < 6)
                    {
                        NivelMat3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriMat3) >= 6 && Convert.ToDouble(PromTriMat3) < 8)
                        {
                            NivelMat3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat3) >= 8 && Convert.ToDouble(PromTriMat3) < 10)
                            {
                                NivelMat3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat3) == 10)
                                {
                                    NivelMat3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelMat3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ingles-------------------------------------------------
                    double PromIng33 = 0;

                    PromIng33 = Convert.ToDouble(CalifMarz[2]) + Convert.ToDouble(CalifAbri[2]) + Convert.ToDouble(CalifMayo[2]) + Convert.ToDouble(CalifJuni[2]);
                    PromIng33 = PromIng33 / 4;
                    PromTriIng3 = PromIng33.ToString("0.#");//Promedio del tercer trimestre de Ingles

                    //Asignacion del nivel del tercer trimestre de Ingles
                    if (Convert.ToDouble(PromTriIng3) >= 5 && Convert.ToDouble(PromTriIng3) < 6)
                    {
                        NivelIng3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriIng3) >= 6 && Convert.ToDouble(PromTriIng3) < 8)
                        {
                            NivelIng3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng3) >= 8 && Convert.ToDouble(PromTriIng3) < 10)
                            {
                                NivelIng3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng3) == 10)
                                {
                                    NivelIng3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelIng3 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                    double Promcn33 = 0;

                    Promcn33 = Convert.ToDouble(CalifMarz[3]) + Convert.ToDouble(CalifAbri[3]) + Convert.ToDouble(CalifMayo[3]) + Convert.ToDouble(CalifJuni[3]);
                    Promcn33 = Promcn33 / 4;
                    PromTricn3 = Promcn33.ToString("0.#");//Promedio del tercer trimestre de Ciencias

                    //Asignacion del nivel del tercer trimestre de Ciencias
                    if (Convert.ToDouble(PromTricn3) >= 5 && Convert.ToDouble(PromTricn3) < 6)
                    {
                        Nivelcn3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTricn3) >= 6 && Convert.ToDouble(PromTricn3) < 8)
                        {
                            Nivelcn3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn3) >= 8 && Convert.ToDouble(PromTricn3) < 10)
                            {
                                Nivelcn3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn3) == 10)
                                {
                                    Nivelcn3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    Nivelcn3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Geografia-------------------------------------------------
                    double PromGeo33 = 0;

                    PromGeo33 = Convert.ToDouble(CalifMarz[4]) + Convert.ToDouble(CalifAbri[4]) + Convert.ToDouble(CalifMayo[4]) + Convert.ToDouble(CalifJuni[4]);
                    PromGeo33 = PromGeo33 / 4;
                    PromTriGeo3 = PromGeo33.ToString("0.#");//Promedio del tercer trimestre de Geografia

                    //Asignacion del nivel del tercer trimestre de Geografia
                    if (Convert.ToDouble(PromTriGeo3) >= 5 && Convert.ToDouble(PromTriGeo3) < 6)
                    {
                        NivelGeo3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGeo3) >= 6 && Convert.ToDouble(PromTriGeo3) < 8)
                        {
                            NivelGeo3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo3) >= 8 && Convert.ToDouble(PromTriGeo3) < 10)
                            {
                                NivelGeo3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo3) == 10)
                                {
                                    NivelGeo3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGeo3 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Historia--------------------------------------------------------
                    double PromHis33 = 0;

                    PromHis33 = Convert.ToDouble(CalifMarz[5]) + Convert.ToDouble(CalifAbri[5]) + Convert.ToDouble(CalifMayo[5]) + Convert.ToDouble(CalifJuni[5]);
                    PromHis33 = PromHis33 / 4;
                    PromTriHis3 = PromHis33.ToString("0.#");//Promedio del tercer trimestre de Historia

                    //Asignacion del nivel del tercer trimestre de Historia
                    if (Convert.ToDouble(PromTriHis3) >= 5 && Convert.ToDouble(PromTriHis3) < 6)
                    {
                        NivelHis3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriHis3) >= 6 && Convert.ToDouble(PromTriHis3) < 8)
                        {
                            NivelHis3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis3) >= 8 && Convert.ToDouble(PromTriHis3) < 10)
                            {
                                NivelHis3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis3) == 10)
                                {
                                    NivelHis3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelHis3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Formacion Civica y etica-------------------------------------------------
                    double PromCiv33 = 0;

                    PromCiv33 = Convert.ToDouble(CalifMarz[6]) + Convert.ToDouble(CalifAbri[6]) + Convert.ToDouble(CalifMayo[6]) + Convert.ToDouble(CalifJuni[6]);
                    PromCiv33 = PromCiv33 / 4;
                    PromTriCiv3 = PromCiv33.ToString("0.#");//Promedio del tercer trimestre de Formacion Civica y etica

                    //Asignacion del nivel del tercer trimestre de Formacion Civica y etica
                    if (Convert.ToDouble(PromTriCiv3) >= 5 && Convert.ToDouble(PromTriCiv3) < 6)
                    {
                        NivelCiv3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCiv3) >= 6 && Convert.ToDouble(PromTriCiv3) < 8)
                        {
                            NivelCiv3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv3) >= 8 && Convert.ToDouble(PromTriCiv3) < 10)
                            {
                                NivelCiv3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv3) == 10)
                                {
                                    NivelCiv3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelCiv3 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Artes--------------------------------------------------------
                    double PromArt33 = 0;

                    PromArt33 = Convert.ToDouble(CalifMarz[7]) + Convert.ToDouble(CalifAbri[7]) + Convert.ToDouble(CalifMayo[7]) + Convert.ToDouble(CalifJuni[7]);
                    PromArt33 = PromArt33 / 4;
                    PromTriArt3 = PromArt33.ToString("0.#");//Promedio del tercer trimestre de Artes

                    //Asignacion del nivel del tercer trimestre de Artes
                    if (Convert.ToDouble(PromTriArt3) >= 5 && Convert.ToDouble(PromTriArt3) < 6)
                    {
                        NivelArt3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriArt3) >= 6 && Convert.ToDouble(PromTriArt3) < 8)
                        {
                            NivelArt3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt3) >= 8 && Convert.ToDouble(PromTriArt3) < 10)
                            {
                                NivelArt3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt3) == 10)
                                {
                                    NivelArt3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelArt3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Ed. Fisica-------------------------------------------------
                    double PromFis33 = 0;

                    PromFis33 = Convert.ToDouble(CalifMarz[8]) + Convert.ToDouble(CalifAbri[8]) + Convert.ToDouble(CalifMayo[8]) + Convert.ToDouble(CalifJuni[8]);
                    PromFis33 = PromFis33 / 4;
                    PromTriFis3 = PromFis33.ToString("0.#");//Promedio del tercer trimestre de Ed. Fisica

                    //Asignacion del nivel del tercer trimestre de Ed. Fisica
                    if (Convert.ToDouble(PromTriFis3) >= 5 && Convert.ToDouble(PromTriFis3) < 6)
                    {
                        NivelFis3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriFis3) >= 6 && Convert.ToDouble(PromTriFis3) < 8)
                        {
                            NivelFis3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis3) >= 8 && Convert.ToDouble(PromTriFis3) < 10)
                            {
                                NivelFis3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis3) == 10)
                                {
                                    NivelFis3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelFis3 = " ";
                                }
                            }
                        }
                    }
                    //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                    double PromSoc33 = 0;

                    PromSoc33 = Convert.ToDouble(CalifMarz[9]) + Convert.ToDouble(CalifAbri[9]) + Convert.ToDouble(CalifMayo[9]) + Convert.ToDouble(CalifJuni[9]);
                    PromSoc33 = PromSoc33 / 4;
                    PromTriSoc3 = PromSoc33.ToString("0.#");//Promedio del tercer trimestre de Ed. Socioemocional

                    //Asignacion del nivel del tercer trimestre de Ed. Socioemocional
                    if (Convert.ToDouble(PromTriSoc3) >= 5 && Convert.ToDouble(PromTriSoc3) < 6)
                    {
                        NivelSoc3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSoc3) >= 6 && Convert.ToDouble(PromTriSoc3) < 8)
                        {
                            NivelSoc3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc3) >= 8 && Convert.ToDouble(PromTriSoc3) < 10)
                            {
                                NivelSoc3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc3) == 10)
                                {
                                    NivelSoc3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelSoc3 = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                    double PromDes33 = 0;

                    PromDes33 = Convert.ToDouble(PromTriArt3) + Convert.ToDouble(PromTriFis3) + Convert.ToDouble(PromTriSoc3);
                    PromDes33 = PromDes33 / 3;
                    PromTriDes3 = PromDes33.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriDes3) >= 5 && Convert.ToDouble(PromTriDes3) < 6)
                    {
                        NivelPromDes3 = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriDes3) >= 6 && Convert.ToDouble(PromTriDes3) < 8)
                        {
                            NivelPromDes3 = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes3) >= 8 && Convert.ToDouble(PromTriDes3) < 10)
                            {
                                NivelPromDes3 = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes3) == 10)
                                {
                                    NivelPromDes3 = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromDes3 = " ";
                                }
                            }
                        }
                    }

                    //-----------------------------Inasistencia-----------------------------------------------------
                    double SumaInasis33 = 0;

                    SumaInasis33 = Convert.ToDouble(CalifMarz[10]) + Convert.ToDouble(CalifAbri[10]) + Convert.ToDouble(CalifMayo[10]) + Convert.ToDouble(CalifJuni[10]);
                    SumaInasisTri3 = SumaInasis33.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                    //------------------------------------Promedio de materias sencillas final trimestres-------------------------------------------------
                    double PromSenFin = 0;

                    PromSenFin = Convert.ToDouble(PromTriSen1) + Convert.ToDouble(PromTriSen2) + Convert.ToDouble(PromTriSen3);
                    PromSenFin = PromSenFin / 3;
                    PromTriSenFin = PromSenFin.ToString("0.#");//Promedio del segundo trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriSenFin) >= 5 && Convert.ToDouble(PromTriSenFin) < 6)
                    {
                        NivelPromSenFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriSenFin) >= 6 && Convert.ToDouble(PromTriSenFin) < 8)
                        {
                            NivelPromSenFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSenFin) >= 8 && Convert.ToDouble(PromTriSenFin) < 10)
                            {
                                NivelPromSenFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSenFin) == 10)
                                {
                                    NivelPromSenFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromSenFin = " ";
                                }
                            }
                        }
                    }

                    //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                    double PromGen3Fin = 0;

                    PromGen3Fin = Convert.ToDouble(PromTriGen1) + Convert.ToDouble(PromTriGen2) + Convert.ToDouble(PromTriGen3);
                    PromGen3Fin = PromGen3Fin / 3;
                    PromTriGenFin = PromGen3Fin.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriGenFin) >= 5 && Convert.ToDouble(PromTriGenFin) < 6)
                    {
                        NivelGenFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGenFin) >= 6 && Convert.ToDouble(PromTriGenFin) < 8)
                        {
                            NivelGenFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGenFin) >= 8 && Convert.ToDouble(PromTriGenFin) < 10)
                            {
                                NivelGenFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGenFin) == 10)
                                {
                                    NivelGenFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGenFin = " ";
                                }
                            }
                        }
                    }

                    if (PromGen3Fin >= 6)
                    {
                        Si = "X";
                        No = " ";
                    }
                    else
                    {
                        Si = " ";
                        No = "X";
                    }
                    //--------------------------------------------Nivel general mayor a II ---------------------------------
                    if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                    {
                        Yes4 = "Yes";
                        Nou4 = "Off";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                        {
                            Yes4 = "Yes";
                            Nou4 = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                            {
                                Yes4 = "Yes";
                                Nou4 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen3) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                                {
                                    Yes4 = "Yes";
                                    Nou4 = "Off";
                                }
                                else
                                {
                                    Yes4 = "Off";
                                    Nou4 = "Yes";
                                }
                            }
                        }
                    }

                    //--------------------------------------Promedio final de español trimestral--------------------------------
                    double PromFin = 0;

                    PromFin = Convert.ToDouble(PromTriEsp1) + Convert.ToDouble(PromTriEsp2) + Convert.ToDouble(PromTriEsp3);
                    PromFin = PromFin / 3;
                    PromFinTriEsp = PromFin.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriEsp) >= 5 && Convert.ToDouble(PromFinTriEsp) < 6)
                    {
                        NivelFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriEsp) >= 6 && Convert.ToDouble(PromFinTriEsp) < 8)
                        {
                            NivelFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriEsp) >= 8 && Convert.ToDouble(PromFinTriEsp) < 10)
                            {
                                NivelFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriEsp) == 10)
                                {
                                    NivelFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelFin = " ";
                                }
                            }
                        }
                    }

                    if (Convert.ToDouble(PromFinTriEsp) > 5.9 && Convert.ToDouble(PromFinTriEsp) <= 10)
                    {
                        Yes = "Yes";
                        Nou = "Off";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriEsp) <= 5.9)
                        {
                            Yes = "Off";
                            Nou = "Yes";
                        }
                    }
                    //--------------------------------------Promedio final de matematicas trimestral--------------------------------
                    double PromFinMat = 0;

                    PromFinMat = Convert.ToDouble(PromTriMat1) + Convert.ToDouble(PromTriMat2) + Convert.ToDouble(PromTriMat3);
                    PromFinMat = PromFinMat / 3;
                    PromFinTriMat = PromFinMat.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriMat) >= 5 && Convert.ToDouble(PromFinTriMat) < 6)
                    {
                        NivelMatFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriMat) >= 6 && Convert.ToDouble(PromFinTriMat) < 8)
                        {
                            NivelMatFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriMat) >= 8 && Convert.ToDouble(PromFinTriMat) < 10)
                            {
                                NivelMatFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriMat) == 10)
                                {
                                    NivelMatFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelMatFin = " ";
                                }
                            }
                        }
                    }

                    if (Convert.ToDouble(PromFinTriMat) > 5.9 && Convert.ToDouble(PromFinTriMat) <= 10)
                    {
                        Yes1 = "Yes";
                        Nou1 = "Off";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriMat) <= 5.9)
                        {
                            Yes1 = "Off";
                            Nou1 = "Yes";
                        }
                    }
                    //--------------------------------------Promedio final de Ingles trimestral--------------------------------
                    double PromFinIng = 0;

                    PromFinIng = Convert.ToDouble(PromTriIng1) + Convert.ToDouble(PromTriIng2) + Convert.ToDouble(PromTriIng3);
                    PromFinIng = PromFinIng / 3;
                    PromFinTriIng = PromFinIng.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriIng) >= 5 && Convert.ToDouble(PromFinTriIng) < 6)
                    {
                        NivelIngFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriIng) >= 6 && Convert.ToDouble(PromFinTriIng) < 8)
                        {
                            NivelIngFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriIng) >= 8 && Convert.ToDouble(PromFinTriIng) < 10)
                            {
                                NivelIngFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriIng) == 10)
                                {
                                    NivelIngFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelIngFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Ciencias Naturales trimestral--------------------------------
                    double PromFincn = 0;

                    PromFincn = Convert.ToDouble(PromTricn1) + Convert.ToDouble(PromTricn2) + Convert.ToDouble(PromTricn3);
                    PromFincn = PromFincn / 3;
                    PromFinTricn = PromFincn.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTricn) >= 5 && Convert.ToDouble(PromFinTricn) < 6)
                    {
                        NivelcnFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTricn) >= 6 && Convert.ToDouble(PromFinTricn) < 8)
                        {
                            NivelcnFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTricn) >= 8 && Convert.ToDouble(PromFinTricn) < 10)
                            {
                                NivelcnFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTricn) == 10)
                                {
                                    NivelcnFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelcnFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Geografia trimestral--------------------------------
                    double PromFinGeo = 0;

                    PromFinGeo = Convert.ToDouble(PromTriGeo1) + Convert.ToDouble(PromTriGeo2) + Convert.ToDouble(PromTriGeo3);
                    PromFinGeo = PromFinGeo / 3;
                    PromFinTriGeo = PromFinGeo.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriGeo) >= 5 && Convert.ToDouble(PromFinTriGeo) < 6)
                    {
                        NivelGeoFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriGeo) >= 6 && Convert.ToDouble(PromFinTriGeo) < 8)
                        {
                            NivelGeoFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriGeo) >= 8 && Convert.ToDouble(PromFinTriGeo) < 10)
                            {
                                NivelGeoFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriGeo) == 10)
                                {
                                    NivelGeoFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelGeoFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Historia trimestral--------------------------------
                    double PromFinHis = 0;

                    PromFinHis = Convert.ToDouble(PromTriHis1) + Convert.ToDouble(PromTriHis2) + Convert.ToDouble(PromTriHis3);
                    PromFinHis = PromFinHis / 3;
                    PromFinTriHis = PromFinHis.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriHis) >= 5 && Convert.ToDouble(PromFinTriHis) < 6)
                    {
                        NivelHisFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriHis) >= 6 && Convert.ToDouble(PromFinTriHis) < 8)
                        {
                            NivelHisFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriHis) >= 8 && Convert.ToDouble(PromFinTriHis) < 10)
                            {
                                NivelHisFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriHis) == 10)
                                {
                                    NivelHisFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelHisFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Formacion Civica y etica trimestral--------------------------------
                    double PromFinCiv = 0;

                    PromFinCiv = Convert.ToDouble(PromTriCiv1) + Convert.ToDouble(PromTriCiv2) + Convert.ToDouble(PromTriCiv3);
                    PromFinCiv = PromFinCiv / 3;
                    PromFinTriCiv = PromFinCiv.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriCiv) >= 5 && Convert.ToDouble(PromFinTriCiv) < 6)
                    {
                        NivelCivFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriCiv) >= 6 && Convert.ToDouble(PromFinTriCiv) < 8)
                        {
                            NivelCivFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriCiv) >= 8 && Convert.ToDouble(PromFinTriCiv) < 10)
                            {
                                NivelCivFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriCiv) == 10)
                                {
                                    NivelCivFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelCivFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Artes trimestral--------------------------------
                    double PromFinArt = 0;

                    PromFinArt = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriArt3);
                    PromFinArt = PromFinArt / 3;
                    PromFinTriArt = PromFinArt.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriArt) >= 5 && Convert.ToDouble(PromFinTriArt) < 6)
                    {
                        NivelArtFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriArt) >= 6 && Convert.ToDouble(PromFinTriArt) < 8)
                        {
                            NivelArtFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriArt) >= 8 && Convert.ToDouble(PromFinTriArt) < 10)
                            {
                                NivelArtFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriArt) == 10)
                                {
                                    NivelArtFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelArtFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Ed. Fisica trimestral--------------------------------
                    double PromFinFis = 0;

                    PromFinFis = Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriFis3);
                    PromFinFis = PromFinFis / 3;
                    PromFinTriFis = PromFinFis.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriFis) >= 5 && Convert.ToDouble(PromFinTriFis) < 6)
                    {
                        NivelFisFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriFis) >= 6 && Convert.ToDouble(PromFinTriFis) < 8)
                        {
                            NivelFisFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriFis) >= 8 && Convert.ToDouble(PromFinTriFis) < 10)
                            {
                                NivelFisFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriFis) == 10)
                                {
                                    NivelFisFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelFisFin = " ";
                                }
                            }
                        }
                    }
                    //--------------------------------------Promedio final de Ed. Socioemocional trimestral--------------------------------
                    double PromFinSoc = 0;

                    PromFinSoc = Convert.ToDouble(PromTriSoc1) + Convert.ToDouble(PromTriSoc2) + Convert.ToDouble(PromTriSoc3);
                    PromFinSoc = PromFinSoc / 3;
                    PromFinTriSoc = PromFinSoc.ToString("0.#");//Promedio final de los trimestres de español

                    //Asignacion del nivel de los trimestres de español
                    if (Convert.ToDouble(PromFinTriSoc) >= 5 && Convert.ToDouble(PromFinTriSoc) < 6)
                    {
                        NivelSocFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriSoc) >= 6 && Convert.ToDouble(PromFinTriSoc) < 8)
                        {
                            NivelSocFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriSoc) >= 8 && Convert.ToDouble(PromFinTriSoc) < 10)
                            {
                                NivelSocFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriSoc) == 10)
                                {
                                    NivelSocFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelSocFin = " ";
                                }
                            }
                        }
                    }
                    //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                    double PromDesFin = 0;

                    PromDesFin = Convert.ToDouble(PromFinTriArt) + Convert.ToDouble(PromFinTriFis) + Convert.ToDouble(PromFinTriSoc);
                    PromDesFin = PromDesFin / 3;
                    PromTriDesFin = PromDesFin.ToString("0.#");//Promedio del primer trimestre 

                    //Asignacion del nivel del primer trimestre 
                    if (Convert.ToDouble(PromTriDesFin) >= 5 && Convert.ToDouble(PromTriDesFin) < 6)
                    {
                        NivelPromDesFin = "I";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriDesFin) >= 6 && Convert.ToDouble(PromTriDesFin) < 8)
                        {
                            NivelPromDesFin = "II";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDesFin) >= 8 && Convert.ToDouble(PromTriDesFin) < 10)
                            {
                                NivelPromDesFin = "III";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDesFin) == 10)
                                {
                                    NivelPromDesFin = "IV";
                                }
                                else
                                {
                                    MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                    NivelPromDesFin = " ";
                                }
                            }
                        }
                    }

                    //-----------------------------si el nivel es mas alto en el desarrollo -------------------------------
                    if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                    {
                        Yes3 = "Yes";
                        Nou3 = "Off";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                        {
                            Yes3 = "Yes";
                            Nou3 = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                            {
                                Yes3 = "Yes";
                                Nou3 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriSoc) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                                {
                                    Yes3 = "Yes";
                                    Nou3 = "Off";
                                }
                                else
                                {
                                    Yes3 = "Off";
                                    Nou3 = "Yes";
                                }
                            }
                        }
                    }
                    //--------------------Promedio de materias de formacion de ingles a conocimiento -------------------------------------------------
                    double PromMaterias = 0;

                    PromMaterias = Convert.ToDouble(PromFinTriIng) + Convert.ToDouble(PromFinTricn) + Convert.ToDouble(PromFinTriGeo) + Convert.ToDouble(PromFinTriHis) + Convert.ToDouble(PromFinTriCiv);
                    PromMaterias = PromMaterias / 5;
                    PromMateriasIC = PromMaterias.ToString("0.#");//Promedio del primer trimestre 

                    if (Convert.ToDouble(PromMateriasIC) >= 6)
                    {
                        Yes2 = "Yes";
                        Nou2 = "Off";
                    }
                    else
                    {
                        Yes2 = "Off";
                        Nou2 = "Yes";
                    }
                    //-----------------------------Numero final de Inasistencia-----------------------------------------------------
                    double SumaInasisFin = 0;
                    double SumaAsist = 0;
                    double PorcAsis = 0;

                    SumaInasisFin = Convert.ToDouble(SumaInasisTri1) + Convert.ToDouble(SumaInasisTri2) + Convert.ToDouble(SumaInasisTri3);
                    SumInasisTriFin = SumaInasisFin.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                    SumaAsist = 185 - SumaInasisFin;
                    SumAsisTriFin = SumaAsist.ToString("0.#");

                    PorcAsis = SumaAsist / 185;
                    PorcAsis = PorcAsis * 100;
                    PorcAsistencias = PorcAsis.ToString("0.##");
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        CalifJuni[i] = " ";
                    }
                }
                conn10.Close();

                //Diagnostico-----------------------------------------------------------------
                string CalifDig = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diagnostico'";

                MySqlConnection conn11;
                MySqlCommand com11;

                conn11 = new MySqlConnection(conexion);
                conn11.Open();

                com11 = new MySqlCommand(CalifDig, conn11);

                MySqlDataReader myreader11 = com11.ExecuteReader();

                string[] CalifDiag = new string[11];
                string PromDiag = " ";
                string PromDiagGeneral = " ";

                if (myreader11.HasRows) //Checa si las celdas estan vacias
                {
                    int L = 0;
                    while (myreader11.Read())//Agrega calificaciones
                    {
                        double[] CalifDiagtemp = new double[11];
                        CalifDiagtemp[L] = Convert.ToDouble(myreader11["CalificacionMen"]);
                        CalifDiag[L] = CalifDiagtemp[L].ToString("0.#");
                        L++;
                    }

                    double PromDiagTemp = 0;

                    for (int i = 0; i < 9; i++)
                    {
                        PromDiagTemp = PromDiagTemp + Convert.ToDouble(CalifDiag[i]);
                    }

                    PromDiagTemp = PromDiagTemp / 9;
                    PromDiag = PromDiagTemp.ToString("0.#");//Promedio de diagnostico

                    double PromDiagGen = 0;

                    PromDiagGen = Convert.ToDouble(PromDiag) + Convert.ToDouble(CalifDiag[9]);
                    PromDiagGen = PromDiagGen / 2;
                    PromDiagGeneral = PromDiagGen.ToString("0.#"); //Promedio general de diagnostico
                }
                else
                {
                    for (int i = 0; i < 11; i++)
                    {
                        CalifDiag[i] = " ";
                    }
                }
                conn11.Close();
                //--------------------------------------------------------------------------------------------------------
                string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                if (!Directory.Exists(folderPath))// pregunt si no existe
                {
                    Directory.CreateDirectory(folderPath); // si no existe lo crea
                }
                string inputFile = Path.Combine(folderPath, "4_PRIMARIA_1819.pdf");
                string outputFile = Path.Combine(folderPath, "BoletaExterna4.pdf");

                PdfReader pdfReader = new PdfReader(inputFile);
                using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (PdfStamper stamper = new PdfStamper(pdfReader, fs))
                    {
                        AcroFields fields = stamper.AcroFields;
                        //fields.SetFieldProperty("PRIMER APELLIDO", "fontsize", 14, null);
                        fields.SetField("PRIMER APELLIDO", Apellidop1);
                        fields.SetField("SEGUNDO APELLIDO", Apellidom1);
                        fields.SetField("NOMBRE(S)", nombre1);
                        fields.SetField("CURP", sesion.Curp);
                        fields.SetField("NOMBRE DE LA ESCUELA", "INSTITUTO RODOLFO NERI VELA");
                        fields.SetField("GRUPO", "A");
                        fields.SetField("TURNO", "MATUTINO");
                        fields.SetField("CCT", "12DPT0003N");


                        fields.SetField("Calendario Escolar", "2018- 2019");
                        fields.SetField("Asistencias", SumAsisTriFin);
                        fields.SetField("Inasistencias", SumInasisTriFin);
                        fields.SetField("CRITERIO DE ACREDITACIÓN % Asistencia", PorcAsistencias, " %");

                        //Si aprobo o no
                        fields.SetField("TextField", Si);
                        fields.SetField("TextField_1", No);

                        fields.SetField("PROMEDIO FINAL DEL NIVELDE DESEMPEÑO DEL GRADO", PromTriGenFin);

                        //ESPAÑOL
                        fields.SetField("Nivel de desempeño", Nivel1);
                        fields.SetField("Nivel de desempeño_1", Nivel2);
                        fields.SetField("Nivel de desempeño_2", Nivel3);
                        fields.SetField("Nivel de desempeño_3", NivelFin);

                        fields.SetField("Calificación", PromTriEsp1);
                        fields.SetField("Calificación_1", PromTriEsp2);
                        fields.SetField("Calificación_2", PromTriEsp3);
                        fields.SetField("Calificación_3", PromFinTriEsp);

                        //Check box
                        fields.SetField("CheckBox", Yes);
                        fields.SetField("CheckBox_1", Nou);

                        //MATEMATICAS
                        fields.SetField("Nivel de desempeño_4", NivelMat1);
                        fields.SetField("Nivel de desempeño_5", NivelMat2);
                        fields.SetField("Nivel de desempeño_6", NivelMat3);
                        fields.SetField("Nivel de desempeño_7", NivelMatFin);

                        fields.SetField("Calificación_4", PromTriMat1);
                        fields.SetField("Calificación_5", PromTriMat2);
                        fields.SetField("Calificación_6", PromTriMat3);
                        fields.SetField("Calificación_7", PromFinTriMat);

                        //Check box
                        fields.SetField("CheckBox_2", Yes1);
                        fields.SetField("CheckBox_3", Nou1);

                        //INGLES
                        fields.SetField("Nivel de desempeño_8", NivelIng1);
                        fields.SetField("Nivel de desempeño_9", NivelIng2);
                        fields.SetField("Nivel de desempeño_10", NivelIng3);
                        fields.SetField("Nivel de desempeño_11", NivelIngFin);

                        fields.SetField("Calificación_8", PromTriIng1);
                        fields.SetField("Calificación_9", PromTriIng2);
                        fields.SetField("Calificación_10", PromTriIng3);
                        fields.SetField("Calificación_11", PromFinTriIng);

                        //CIENCIAS NATURALES
                        fields.SetField("Nivel de desempeño_12", Nivelcn1);
                        fields.SetField("Nivel de desempeño_13", Nivelcn2);
                        fields.SetField("Nivel de desempeño_14", Nivelcn3);
                        fields.SetField("Nivel de desempeño_15", NivelcnFin);

                        fields.SetField("Calificación_12", PromTricn1);
                        fields.SetField("Calificación_13", PromTricn2);
                        fields.SetField("Calificación_14", PromTricn3);
                        fields.SetField("Calificación_15", PromFinTricn);

                        //GEOGRAFIA
                        fields.SetField("Nivel de desempeño_16", NivelGeo1);
                        fields.SetField("Nivel de desempeño_17", NivelGeo2);
                        fields.SetField("Nivel de desempeño_18", NivelGeo3);
                        fields.SetField("Nivel de desempeño_19", NivelGeoFin);

                        fields.SetField("Calificación_16", PromTriGeo1);
                        fields.SetField("Calificación_17", PromTriGeo2);
                        fields.SetField("Calificación_18", PromTriGeo3);
                        fields.SetField("Calificación_19", PromFinTriGeo);

                        //HISTORIA
                        fields.SetField("Nivel de desempeño_20", NivelHis1);
                        fields.SetField("Nivel de desempeño_21", NivelHis2);
                        fields.SetField("Nivel de desempeño_22", NivelHis3);
                        fields.SetField("Nivel de desempeño_23", NivelHisFin);

                        fields.SetField("Calificación_20", PromTriHis1);
                        fields.SetField("Calificación_21", PromTriHis2);
                        fields.SetField("Calificación_22", PromTriHis3);
                        fields.SetField("Calificación_23", PromFinTriHis);

                        //FORMACION CIVICA Y ETICA
                        fields.SetField("Nivel de desempeño_24", NivelCiv1);
                        fields.SetField("Nivel de desempeño_25", NivelCiv2);
                        fields.SetField("Nivel de desempeño_26", NivelCiv3);
                        fields.SetField("Nivel de desempeño_27", NivelCivFin);

                        fields.SetField("Calificación_24", PromTriCiv1);
                        fields.SetField("Calificación_25", PromTriCiv2);
                        fields.SetField("Calificación_26", PromTriCiv3);
                        fields.SetField("Calificación_27", PromFinTriCiv);

                        //Check box
                        fields.SetField("CheckBox_4", Yes2);
                        fields.SetField("CheckBox_5", Nou2);


                        fields.SetField("TextField_2", PromMateriasIC);

                        //Promedio formacion academica 
                        fields.SetField("Nivel de desempeño_28", NivelPromSen);
                        fields.SetField("Nivel de desempeño_29", NivelPromSen2);
                        fields.SetField("Nivel de desempeño_30", NivelPromSen3);
                        fields.SetField("Nivel de desempeño_31", NivelPromSenFin);

                        fields.SetField("Calificación_28", PromTriSen1);
                        fields.SetField("Calificación_29", PromTriSen2);
                        fields.SetField("Calificación_30", PromTriSen3);
                        fields.SetField("Calificación_31", PromFinTriMat);

                        //ARTES
                        fields.SetField("ARTES", NivelArt1);
                        fields.SetField("ARTES_1", NivelArt2);
                        fields.SetField("ARTES_2", NivelArt3);
                        fields.SetField("ARTES_3", NivelArtFin);

                        //EDUCACION SOCIOEMOCIONAL 
                        fields.SetField("EDUCACIÓNSOCIOEMOCIONAL", NivelSoc1);
                        fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_1", NivelSoc2);
                        fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_2", NivelSoc3);
                        fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_3", NivelSocFin);

                        //EDUCACION FISICA
                        fields.SetField("EDUCACIÓN FÍSICA", NivelFis1);
                        fields.SetField("EDUCACIÓN FÍSICA_1", NivelFis2);
                        fields.SetField("EDUCACIÓN FÍSICA_2", NivelFis3);
                        fields.SetField("EDUCACIÓN FÍSICA_3", NivelFisFin);

                        //Check box
                        fields.SetField("CheckBox_6", Yes3);
                        fields.SetField("CheckBox_7", Nou3);

                        //Promedio desarrollo social 
                        fields.SetField("PROMEDIOFINAL", NivelPromDes);
                        fields.SetField("PROMEDIOFINAL_1", NivelPromDes2);
                        fields.SetField("PROMEDIOFINAL_2", NivelPromDes3);
                        fields.SetField("PROMEDIOFINAL_3", NivelPromDesFin);

                        //Promedio general
                        fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL", NivelGen1);
                        fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_1", NivelGen2);
                        fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_2", NivelGen3);
                        fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_3", NivelGenFin);

                        //Check box
                        fields.SetField("CheckBox_8", Yes4);
                        fields.SetField("CheckBox_9", Nou4);

                        fields.SetField("LUGAR DE EXPEDICIÓN", "Acapulco, Gro.");
                        fields.SetField("AÑO", "2019");
                        fields.SetField("TextField_4", " ");
                        fields.SetField("DÍA", " ");

                        stamper.Close();
                    }
                }
                pdfReader.Close();

                MessageBox.Show("¡PDF creado!");
            }
            else
            {
                if (Convert.ToInt32(sesion.grado) == 5)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
                    string nombre1, Apellidop1, Apellidom1, IdAlumno1;

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    myreader.Read();
                    nombre1 = Convert.ToString(myreader["nombre"]);
                    Apellidop1 = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom1 = Convert.ToString(myreader["ApellidoM"]);
                    sesion.grado = Convert.ToString(myreader["idGrado"]);
                    IdAlumno1 = Convert.ToString(myreader["idAlumno"]);
                    conn.Close();

                    //-------------------------------Ingresar las calificaciones mensuales de los alumnos---------------------

                    //Septiembre------------------------------------------
                    string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Septiembre'";

                    MySqlConnection conn1;
                    MySqlCommand com1;

                    conn1 = new MySqlConnection(conexion);
                    conn1.Open();

                    com1 = new MySqlCommand(CalifSep, conn1);

                    MySqlDataReader myreader1 = com1.ExecuteReader();

                    string[] CalifSept = new string[11];
                    string PromSept = " ";
                    string PromSeptGeneral = " ";

                    if (myreader1.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader1.Read())//Agrega calificaciones
                        {
                            double[] CalifSeptemp = new double[11];
                            CalifSeptemp[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                            CalifSept[L] = CalifSeptemp[L].ToString("0.#");
                            L++;
                        }

                        double PromSeptTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromSeptTemp = PromSeptTemp + Convert.ToDouble(CalifSept[i]);
                        }

                        PromSeptTemp = PromSeptTemp / 9;
                        PromSept = PromSeptTemp.ToString("0.#"); //Promedio de septiembre

                        double PromSeptGen = 0;

                        PromSeptGen = Convert.ToDouble(PromSept) + Convert.ToDouble(CalifSept[9]);
                        PromSeptGen = PromSeptGen / 2;
                        PromSeptGeneral = PromSeptGen.ToString("0.#"); //Promedio general de semptiembre
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifSept[i] = " ";
                        }
                    }
                    conn1.Close();

                    //Octubre-----------------------------------------------------------------
                    string CalifOct = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Octubre'";

                    MySqlConnection conn2;
                    MySqlCommand com2;

                    conn2 = new MySqlConnection(conexion);
                    conn2.Open();

                    com2 = new MySqlCommand(CalifOct, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    string[] CalifOctu = new string[11];
                    string PromOct = " ";
                    string PromOctuGeneral = " ";

                    if (myreader2.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader2.Read())//Agrega calificaciones
                        {
                            double[] CalifOctutemp = new double[11];
                            CalifOctutemp[L] = Convert.ToDouble(myreader2["CalificacionMen"]);
                            CalifOctu[L] = CalifOctutemp[L].ToString("0.#");
                            L++;
                        }

                        double PromOctTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromOctTemp = PromOctTemp + Convert.ToDouble(CalifOctu[i]);
                        }

                        PromOctTemp = PromOctTemp / 9;
                        PromOct = PromOctTemp.ToString("0.#"); //Promedio de octubre

                        double PromOctGen = 0;

                        PromOctGen = Convert.ToDouble(PromOct) + Convert.ToDouble(CalifOctu[9]);
                        PromOctGen = PromOctGen / 2;
                        PromOctuGeneral = PromOctGen.ToString("0.#"); //Promedio general de semptiembre
                    }
                    else
                    {

                        for (int i = 0; i < 11; i++)
                        {
                            CalifOctu[i] = " ";
                        }
                    }
                    conn2.Close();
                    //Noviembre-----------------------------------------------------------------
                    string CalifNov = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Noviembre'";

                    MySqlConnection conn3;
                    MySqlCommand com3;

                    conn3 = new MySqlConnection(conexion);
                    conn3.Open();

                    com3 = new MySqlCommand(CalifNov, conn3);

                    MySqlDataReader myreader3 = com3.ExecuteReader();

                    string[] CalifNovi = new string[11];
                    string PromNov = " ";
                    string PromTriSen1 = " ";
                    string PromTriEsp1 = " ";
                    string PromTriMat1 = " ";
                    string PromTriIng1 = " ";
                    string PromTricn1 = " ";
                    string PromTriGeo1 = " ";
                    string PromTriHis1 = " ";
                    string PromTriCiv1 = " ";
                    string PromTriArt1 = " ";
                    string PromTriFis1 = " ";
                    string PromTriSoc1 = " ";
                    string SumaInasisTri1 = " ";
                    string NivelPromSen = " ";
                    string Nivel1 = " ";
                    string NivelMat1 = " ";
                    string NivelIng1 = " ";
                    string Nivelcn1 = " ";
                    string NivelGeo1 = " ";
                    string NivelHis1 = " ";
                    string NivelCiv1 = " ";
                    string NivelArt1 = " ";
                    string NivelFis1 = " ";
                    string NivelSoc1 = " ";
                    string PromNoviGeneral = " ";
                    string PromTriGen1 = " ";
                    string NivelGen1 = " ";

                    string NivelPromDes = " ";
                    string PromTriDes1 = " ";

                    if (myreader3.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader3.Read())//Agrega calificaciones
                        {
                            double[] CalifNovitemp = new double[11];
                            CalifNovitemp[L] = Convert.ToDouble(myreader3["CalificacionMen"]);
                            CalifNovi[L] = CalifNovitemp[L].ToString("0.#");
                            L++;
                        }

                        double PromNovTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromNovTemp = PromNovTemp + Convert.ToDouble(CalifNovi[i]);
                        }

                        PromNovTemp = PromNovTemp / 9;
                        PromNov = PromNovTemp.ToString("0.#");//Promedio de noviembre

                        double PromNovGen = 0;

                        PromNovGen = Convert.ToDouble(PromNov) + Convert.ToDouble(CalifNovi[9]);
                        PromNovGen = PromNovGen / 2;
                        PromNoviGeneral = PromNovGen.ToString("0.#"); //Promedio general de nociembre

                        //------------------------------------Promedio de materias sencillas primer trimestre-------------------------------------------------
                        double PromSen31 = 0;

                        PromSen31 = Convert.ToDouble(PromSept) + Convert.ToDouble(PromOct) + Convert.ToDouble(PromNov);
                        PromSen31 = PromSen31 / 3;
                        PromTriSen1 = PromSen31.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                        {
                            NivelPromSen = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                            {
                                NivelPromSen = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                                {
                                    NivelPromSen = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen1) == 10)
                                    {
                                        NivelPromSen = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromSen = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Promedio General primer trimestre-------------------------------------------------
                        double PromGen31 = 0;

                        PromGen31 = Convert.ToDouble(PromSeptGeneral) + Convert.ToDouble(PromOctuGeneral) + Convert.ToDouble(PromNoviGeneral);
                        PromGen31 = PromGen31 / 3;
                        PromTriGen1 = PromGen31.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriGen1) >= 5 && Convert.ToDouble(PromTriGen1) < 6)
                        {
                            NivelGen1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen1) >= 6 && Convert.ToDouble(PromTriGen1) < 8)
                            {
                                NivelGen1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen1) >= 8 && Convert.ToDouble(PromTriGen1) < 10)
                                {
                                    NivelGen1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen1) == 10)
                                    {
                                        NivelGen1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGen1 = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Español-------------------------------------------------
                        double PromEsp31 = 0;

                        PromEsp31 = Convert.ToDouble(CalifSept[0]) + Convert.ToDouble(CalifOctu[0]) + Convert.ToDouble(CalifNovi[0]);
                        PromEsp31 = PromEsp31 / 3;
                        PromTriEsp1 = PromEsp31.ToString("0.#");//Promedio del primer trimestre de español

                        //Asignacion del nivel del primer trimestre de español
                        if (Convert.ToDouble(PromTriEsp1) >= 5 && Convert.ToDouble(PromTriEsp1) < 6)
                        {
                            Nivel1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp1) >= 6 && Convert.ToDouble(PromTriEsp1) < 8)
                            {
                                Nivel1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp1) >= 8 && Convert.ToDouble(PromTriEsp1) < 10)
                                {
                                    Nivel1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp1) == 10)
                                    {
                                        Nivel1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivel1 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Matematicas--------------------------------------------------------
                        double PromMat31 = 0;

                        PromMat31 = Convert.ToDouble(CalifSept[1]) + Convert.ToDouble(CalifOctu[1]) + Convert.ToDouble(CalifNovi[1]);
                        PromMat31 = PromMat31 / 3;
                        PromTriMat1 = PromMat31.ToString("0.#");//Promedio del primer trimestre de matematicas

                        //Asignacion del nivel del primer trimestre de matematicas
                        if (Convert.ToDouble(PromTriMat1) >= 5 && Convert.ToDouble(PromTriMat1) < 6)
                        {
                            NivelMat1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat1) >= 6 && Convert.ToDouble(PromTriMat1) < 8)
                            {
                                NivelMat1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat1) >= 8 && Convert.ToDouble(PromTriMat1) < 10)
                                {
                                    NivelMat1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat1) == 10)
                                    {
                                        NivelMat1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelMat1 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ingles-------------------------------------------------
                        double PromIng31 = 0;

                        PromIng31 = Convert.ToDouble(CalifSept[2]) + Convert.ToDouble(CalifOctu[2]) + Convert.ToDouble(CalifNovi[2]);
                        PromIng31 = PromIng31 / 3;
                        PromTriIng1 = PromIng31.ToString("0.#");//Promedio del primer trimestre de Ingles

                        //Asignacion del nivel del primer trimestre de Ingles
                        if (Convert.ToDouble(PromTriIng1) >= 5 && Convert.ToDouble(PromTriIng1) < 6)
                        {
                            NivelIng1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng1) >= 6 && Convert.ToDouble(PromTriIng1) < 8)
                            {
                                NivelIng1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng1) >= 8 && Convert.ToDouble(PromTriIng1) < 10)
                                {
                                    NivelIng1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng1) == 10)
                                    {
                                        NivelIng1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelIng1 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                        double Promcn31 = 0;

                        Promcn31 = Convert.ToDouble(CalifSept[3]) + Convert.ToDouble(CalifOctu[3]) + Convert.ToDouble(CalifNovi[3]);
                        Promcn31 = Promcn31 / 3;
                        PromTricn1 = Promcn31.ToString("0.#");//Promedio del primer trimestre de Ciencias

                        //Asignacion del nivel del primer trimestre de Ciencias
                        if (Convert.ToDouble(PromTricn1) >= 5 && Convert.ToDouble(PromTricn1) < 6)
                        {
                            Nivelcn1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn1) >= 6 && Convert.ToDouble(PromTricn1) < 8)
                            {
                                Nivelcn1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn1) >= 8 && Convert.ToDouble(PromTricn1) < 10)
                                {
                                    Nivelcn1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn1) == 10)
                                    {
                                        Nivelcn1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivelcn1 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Geografia-------------------------------------------------
                        double PromGeo31 = 0;

                        PromGeo31 = Convert.ToDouble(CalifSept[4]) + Convert.ToDouble(CalifOctu[4]) + Convert.ToDouble(CalifNovi[4]);
                        PromGeo31 = PromGeo31 / 3;
                        PromTriGeo1 = PromGeo31.ToString("0.#");//Promedio del primer trimestre de Geografia

                        //Asignacion del nivel del primer trimestre de Geografia
                        if (Convert.ToDouble(PromTriGeo1) >= 5 && Convert.ToDouble(PromTriGeo1) < 6)
                        {
                            NivelGeo1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo1) >= 6 && Convert.ToDouble(PromTriGeo1) < 8)
                            {
                                NivelGeo1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo1) >= 8 && Convert.ToDouble(PromTriGeo1) < 10)
                                {
                                    NivelGeo1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo1) == 10)
                                    {
                                        NivelGeo1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGeo1 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Historia--------------------------------------------------------
                        double PromHis31 = 0;

                        PromHis31 = Convert.ToDouble(CalifSept[5]) + Convert.ToDouble(CalifOctu[5]) + Convert.ToDouble(CalifNovi[5]);
                        PromHis31 = PromHis31 / 3;
                        PromTriHis1 = PromHis31.ToString("0.#");//Promedio del primer trimestre de Historia

                        //Asignacion del nivel del primer trimestre de Historia
                        if (Convert.ToDouble(PromTriHis1) >= 5 && Convert.ToDouble(PromTriHis1) < 6)
                        {
                            NivelHis1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis1) >= 6 && Convert.ToDouble(PromTriHis1) < 8)
                            {
                                NivelHis1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis1) >= 8 && Convert.ToDouble(PromTriHis1) < 10)
                                {
                                    NivelHis1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis1) == 10)
                                    {
                                        NivelHis1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelHis1 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Formacion Civica y etica-------------------------------------------------
                        double PromCiv31 = 0;

                        PromCiv31 = Convert.ToDouble(CalifSept[6]) + Convert.ToDouble(CalifOctu[6]) + Convert.ToDouble(CalifNovi[6]);
                        PromCiv31 = PromCiv31 / 3;
                        PromTriCiv1 = PromCiv31.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                        //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                        if (Convert.ToDouble(PromTriCiv1) >= 5 && Convert.ToDouble(PromTriCiv1) < 6)
                        {
                            NivelCiv1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv1) >= 6 && Convert.ToDouble(PromTriCiv1) < 8)
                            {
                                NivelCiv1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv1) >= 8 && Convert.ToDouble(PromTriCiv1) < 10)
                                {
                                    NivelCiv1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv1) == 10)
                                    {
                                        NivelCiv1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelCiv1 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Artes--------------------------------------------------------
                        double PromArt31 = 0;

                        PromArt31 = Convert.ToDouble(CalifSept[7]) + Convert.ToDouble(CalifOctu[7]) + Convert.ToDouble(CalifNovi[7]);
                        PromArt31 = PromArt31 / 3;
                        PromTriArt1 = PromArt31.ToString("0.#");//Promedio del primer trimestre de Artes

                        //Asignacion del nivel del primer trimestre de Artes
                        if (Convert.ToDouble(PromTriArt1) >= 5 && Convert.ToDouble(PromTriArt1) < 6)
                        {
                            NivelArt1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt1) >= 6 && Convert.ToDouble(PromTriArt1) < 8)
                            {
                                NivelArt1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt1) >= 8 && Convert.ToDouble(PromTriArt1) < 10)
                                {
                                    NivelArt1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt1) == 10)
                                    {
                                        NivelArt1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelArt1 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ed. Fisica-------------------------------------------------
                        double PromFis31 = 0;

                        PromFis31 = Convert.ToDouble(CalifSept[8]) + Convert.ToDouble(CalifOctu[8]) + Convert.ToDouble(CalifNovi[8]);
                        PromFis31 = PromFis31 / 3;
                        PromTriFis1 = PromFis31.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                        //Asignacion del nivel del primer trimestre de Ed. Fisica
                        if (Convert.ToDouble(PromTriFis1) >= 5 && Convert.ToDouble(PromTriFis1) < 6)
                        {
                            NivelFis1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis1) >= 6 && Convert.ToDouble(PromTriFis1) < 8)
                            {
                                NivelFis1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis1) >= 8 && Convert.ToDouble(PromTriFis1) < 10)
                                {
                                    NivelFis1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis1) == 10)
                                    {
                                        NivelFis1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelFis1 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                        double PromSoc31 = 0;

                        PromSoc31 = Convert.ToDouble(CalifSept[9]) + Convert.ToDouble(CalifOctu[9]) + Convert.ToDouble(CalifNovi[9]);
                        PromSoc31 = PromSoc31 / 3;
                        PromTriSoc1 = PromSoc31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                        //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                        if (Convert.ToDouble(PromTriSoc1) >= 5 && Convert.ToDouble(PromTriSoc1) < 6)
                        {
                            NivelSoc1 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc1) >= 6 && Convert.ToDouble(PromTriSoc1) < 8)
                            {
                                NivelSoc1 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc1) >= 8 && Convert.ToDouble(PromTriSoc1) < 10)
                                {
                                    NivelSoc1 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc1) == 10)
                                    {
                                        NivelSoc1 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelSoc1 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Promedio de materias desarrollo social primer trimestre-------------------------------------------------
                        double PromDes31 = 0;

                        PromDes31 = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriSoc1);
                        PromDes31 = PromDes31 / 3;
                        PromTriDes1 = PromDes31.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriDes1) >= 5 && Convert.ToDouble(PromTriDes1) < 6)
                        {
                            NivelPromDes = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes1) >= 6 && Convert.ToDouble(PromTriDes1) < 8)
                            {
                                NivelPromDes = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes1) >= 8 && Convert.ToDouble(PromTriDes1) < 10)
                                {
                                    NivelPromDes = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes1) == 10)
                                    {
                                        NivelPromDes = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromDes = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Inasistencias----------------------------------------------------
                        double SumaInasis31 = 0;

                        SumaInasis31 = Convert.ToDouble(CalifSept[10]) + Convert.ToDouble(CalifOctu[10]) + Convert.ToDouble(CalifNovi[10]);
                        SumaInasisTri1 = SumaInasis31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                    }
                    else
                    {

                        for (int i = 0; i < 11; i++)
                        {
                            CalifNovi[i] = " ";
                        }
                    }
                    conn3.Close();

                    //Diciembre-----------------------------------------------------------------
                    string CalifDic = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diciembre'";

                    MySqlConnection conn4;
                    MySqlCommand com4;

                    conn4 = new MySqlConnection(conexion);
                    conn4.Open();

                    com4 = new MySqlCommand(CalifDic, conn4);

                    MySqlDataReader myreader4 = com4.ExecuteReader();

                    string[] CalifDici = new string[11];
                    string PromDic = " ";
                    string PromDiciGeneral = " ";

                    if (myreader4.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader4.Read())//Agrega calificaciones
                        {
                            double[] CalifDicitemp = new double[11];
                            CalifDicitemp[L] = Convert.ToDouble(myreader4["CalificacionMen"]);
                            CalifDici[L] = CalifDicitemp[L].ToString("0.#");
                            L++;
                        }

                        double PromDicTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromDicTemp = PromDicTemp + Convert.ToDouble(CalifDici[i]);
                        }

                        PromDicTemp = PromDicTemp / 9;
                        PromDic = PromDicTemp.ToString("0.#");//Promedio de diciembre

                        double PromDiciGen = 0;

                        PromDiciGen = Convert.ToDouble(PromDic) + Convert.ToDouble(CalifDici[9]);
                        PromDiciGen = PromDiciGen / 2;
                        PromDiciGeneral = PromDiciGen.ToString("0.#"); //Promedio general de diciembre
                    }
                    else
                    {

                        for (int i = 0; i < 11; i++)
                        {
                            CalifDici[i] = " ";
                        }
                    }
                    conn4.Close();
                    //Enero-----------------------------------------------------------------
                    string CalifEne = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Enero'";

                    MySqlConnection conn5;
                    MySqlCommand com5;

                    conn5 = new MySqlConnection(conexion);
                    conn5.Open();

                    com5 = new MySqlCommand(CalifEne, conn5);

                    MySqlDataReader myreader5 = com5.ExecuteReader();

                    string[] CalifEner = new string[11];
                    string PromEne = " ";
                    string PromEnerGeneral = " ";

                    if (myreader5.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader5.Read())//Agrega calificaciones
                        {
                            double[] CalifEnertemp = new double[11];
                            CalifEnertemp[L] = Convert.ToDouble(myreader5["CalificacionMen"]);
                            CalifEner[L] = CalifEnertemp[L].ToString("0.#");
                            L++;
                        }

                        double PromEneTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromEneTemp = PromEneTemp + Convert.ToDouble(CalifEner[i]);
                        }

                        PromEneTemp = PromEneTemp / 9;
                        PromEne = PromEneTemp.ToString("0.#");//Promedio de enero

                        double PromEnerGen = 0;

                        PromEnerGen = Convert.ToDouble(PromEne) + Convert.ToDouble(CalifEner[9]);
                        PromEnerGen = PromEnerGen / 2;
                        PromEnerGeneral = PromEnerGen.ToString("0.#"); //Promedio general de enero
                    }
                    else
                    {

                        for (int i = 0; i < 11; i++)
                        {
                            CalifEner[i] = " ";
                        }
                    }
                    conn5.Close();

                    //Febrero-----------------------------------------------------------------
                    string CalifFeb = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Febrero'";

                    MySqlConnection conn6;
                    MySqlCommand com6;

                    conn6 = new MySqlConnection(conexion);
                    conn6.Open();

                    com6 = new MySqlCommand(CalifFeb, conn6);

                    MySqlDataReader myreader6 = com6.ExecuteReader();

                    string[] CalifFebr = new string[11];
                    string PromFeb = " ";
                    string PromTriEsp2 = " ";
                    string PromTriMat2 = " ";
                    string PromTriIng2 = " ";
                    string PromTricn2 = " ";
                    string PromTriGeo2 = " ";
                    string PromTriHis2 = " ";
                    string PromTriCiv2 = " ";
                    string PromTriArt2 = " ";
                    string PromTriFis2 = " ";
                    string PromTriSoc2 = " ";
                    string PromTriSen2 = " ";
                    string SumaInasisTri2 = " ";
                    string Nivel2 = " ";
                    string NivelMat2 = " ";
                    string NivelIng2 = " ";
                    string Nivelcn2 = " ";
                    string NivelGeo2 = " ";
                    string NivelHis2 = " ";
                    string NivelCiv2 = " ";
                    string NivelArt2 = " ";
                    string NivelFis2 = " ";
                    string NivelSoc2 = " ";
                    string NivelPromSen2 = " ";
                    string PromFebrGeneral = " ";
                    string PromTriGen2 = " ";
                    string NivelGen2 = " ";

                    string NivelPromDes2 = " ";
                    string PromTriDes2 = " ";

                    if (myreader6.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader6.Read())//Agrega calificaciones
                        {
                            double[] CalifFebrtemp = new double[11];
                            CalifFebrtemp[L] = Convert.ToDouble(myreader6["CalificacionMen"]);
                            CalifFebr[L] = CalifFebrtemp[L].ToString("0.#");
                            L++;
                        }

                        double PromFebTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromFebTemp = PromFebTemp + Convert.ToDouble(CalifFebr[i]);
                        }

                        PromFebTemp = PromFebTemp / 9;
                        PromFeb = PromFebTemp.ToString("0.#");//Promedio de febrero

                        double PromFebrGen = 0;

                        PromFebrGen = Convert.ToDouble(PromFeb) + Convert.ToDouble(CalifFebr[9]);
                        PromFebrGen = PromFebrGen / 2;
                        PromFebrGeneral = PromFebrGen.ToString("0.#"); //Promedio general de febrero

                        //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                        double PromSen32 = 0;

                        PromSen32 = Convert.ToDouble(PromEne) + Convert.ToDouble(PromDic) + Convert.ToDouble(PromFeb);
                        PromSen32 = PromSen32 / 3;
                        PromTriSen2 = PromSen32.ToString("0.#");//Promedio del segundo trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                        {
                            NivelPromSen2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                            {
                                NivelPromSen2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                                {
                                    NivelPromSen2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen1) == 10)
                                    {
                                        NivelPromSen2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromSen2 = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Promedio General segundo trimestre-------------------------------------------------
                        double PromGen32 = 0;

                        PromGen32 = Convert.ToDouble(PromDiciGeneral) + Convert.ToDouble(PromEnerGeneral) + Convert.ToDouble(PromFebrGeneral);
                        PromGen32 = PromGen32 / 3;
                        PromTriGen2 = PromGen32.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriGen2) >= 5 && Convert.ToDouble(PromTriGen2) < 6)
                        {
                            NivelGen2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen2) >= 6 && Convert.ToDouble(PromTriGen2) < 8)
                            {
                                NivelGen2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen2) >= 8 && Convert.ToDouble(PromTriGen2) < 10)
                                {
                                    NivelGen2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen2) == 10)
                                    {
                                        NivelGen2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGen2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Español-------------------------------------------------
                        double PromEsp32 = 0;

                        PromEsp32 = Convert.ToDouble(CalifDici[0]) + Convert.ToDouble(CalifEner[0]) + Convert.ToDouble(CalifFebr[0]);
                        PromEsp32 = PromEsp32 / 3;
                        PromTriEsp2 = PromEsp32.ToString("0.#");//Promedio del primer trimestre de español

                        //Asignacion del nivel del primer trimestre de español
                        if (Convert.ToDouble(PromTriEsp2) >= 5 && Convert.ToDouble(PromTriEsp2) < 6)
                        {
                            Nivel2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp2) >= 6 && Convert.ToDouble(PromTriEsp2) < 8)
                            {
                                Nivel2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp2) >= 8 && Convert.ToDouble(PromTriEsp2) < 10)
                                {
                                    Nivel2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp2) == 10)
                                    {
                                        Nivel2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivel2 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Matematicas--------------------------------------------------------
                        double PromMat32 = 0;

                        PromMat32 = Convert.ToDouble(CalifDici[1]) + Convert.ToDouble(CalifEner[1]) + Convert.ToDouble(CalifFebr[1]);
                        PromMat32 = PromMat32 / 3;
                        PromTriMat2 = PromMat32.ToString("0.#");//Promedio del primer trimestre de matematicas

                        //Asignacion del nivel del primer trimestre de matematicas
                        if (Convert.ToDouble(PromTriMat2) >= 5 && Convert.ToDouble(PromTriMat2) < 6)
                        {
                            NivelMat2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat2) >= 6 && Convert.ToDouble(PromTriMat2) < 8)
                            {
                                NivelMat2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat2) >= 8 && Convert.ToDouble(PromTriMat2) < 10)
                                {
                                    NivelMat2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat2) == 10)
                                    {
                                        NivelMat2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelMat2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ingles-------------------------------------------------
                        double PromIng32 = 0;

                        PromIng32 = Convert.ToDouble(CalifDici[2]) + Convert.ToDouble(CalifEner[2]) + Convert.ToDouble(CalifFebr[2]);
                        PromIng32 = PromIng32 / 3;
                        PromTriIng2 = PromIng32.ToString("0.#");//Promedio del primer trimestre de Ingles

                        //Asignacion del nivel del primer trimestre de Ingles
                        if (Convert.ToDouble(PromTriIng2) >= 5 && Convert.ToDouble(PromTriIng2) < 6)
                        {
                            NivelIng2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng2) >= 6 && Convert.ToDouble(PromTriIng2) < 8)
                            {
                                NivelIng2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng2) >= 8 && Convert.ToDouble(PromTriIng2) < 10)
                                {
                                    NivelIng2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng2) == 10)
                                    {
                                        NivelIng2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelIng2 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                        double Promcn32 = 0;

                        Promcn32 = Convert.ToDouble(CalifDici[3]) + Convert.ToDouble(CalifEner[3]) + Convert.ToDouble(CalifFebr[3]);
                        Promcn32 = Promcn32 / 3;
                        PromTricn2 = Promcn32.ToString("0.#");//Promedio del primer trimestre de Ciencias

                        //Asignacion del nivel del primer trimestre de Ciencias
                        if (Convert.ToDouble(PromTricn2) >= 5 && Convert.ToDouble(PromTricn2) < 6)
                        {
                            Nivelcn2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn2) >= 6 && Convert.ToDouble(PromTricn2) < 8)
                            {
                                Nivelcn2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn2) >= 8 && Convert.ToDouble(PromTricn2) < 10)
                                {
                                    Nivelcn2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn2) == 10)
                                    {
                                        Nivelcn2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivelcn2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Geografia-------------------------------------------------
                        double PromGeo32 = 0;

                        PromGeo32 = Convert.ToDouble(CalifDici[4]) + Convert.ToDouble(CalifEner[4]) + Convert.ToDouble(CalifFebr[4]);
                        PromGeo32 = PromGeo32 / 3;
                        PromTriGeo2 = PromGeo32.ToString("0.#");//Promedio del primer trimestre de Geografia

                        //Asignacion del nivel del primer trimestre de Geografia
                        if (Convert.ToDouble(PromTriGeo2) >= 5 && Convert.ToDouble(PromTriGeo2) < 6)
                        {
                            NivelGeo2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo2) >= 6 && Convert.ToDouble(PromTriGeo2) < 8)
                            {
                                NivelGeo2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo2) >= 8 && Convert.ToDouble(PromTriGeo2) < 10)
                                {
                                    NivelGeo2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo2) == 10)
                                    {
                                        NivelGeo2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGeo2 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Historia--------------------------------------------------------
                        double PromHis32 = 0;

                        PromHis32 = Convert.ToDouble(CalifDici[5]) + Convert.ToDouble(CalifEner[5]) + Convert.ToDouble(CalifFebr[5]);
                        PromHis32 = PromHis32 / 3;
                        PromTriHis2 = PromHis32.ToString("0.#");//Promedio del primer trimestre de Historia

                        //Asignacion del nivel del primer trimestre de Historia
                        if (Convert.ToDouble(PromTriHis2) >= 5 && Convert.ToDouble(PromTriHis2) < 6)
                        {
                            NivelHis2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis2) >= 6 && Convert.ToDouble(PromTriHis2) < 8)
                            {
                                NivelHis2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis2) >= 8 && Convert.ToDouble(PromTriHis2) < 10)
                                {
                                    NivelHis2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis2) == 10)
                                    {
                                        NivelHis2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelHis2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Formacion Civica y etica-------------------------------------------------
                        double PromCiv32 = 0;

                        PromCiv32 = Convert.ToDouble(CalifDici[6]) + Convert.ToDouble(CalifEner[6]) + Convert.ToDouble(CalifFebr[6]);
                        PromCiv32 = PromCiv32 / 3;
                        PromTriCiv2 = PromCiv32.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                        //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                        if (Convert.ToDouble(PromTriCiv2) >= 5 && Convert.ToDouble(PromTriCiv2) < 6)
                        {
                            NivelCiv2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv2) >= 6 && Convert.ToDouble(PromTriCiv2) < 8)
                            {
                                NivelCiv2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv2) >= 8 && Convert.ToDouble(PromTriCiv2) < 10)
                                {
                                    NivelCiv2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv2) == 10)
                                    {
                                        NivelCiv2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelCiv2 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Artes--------------------------------------------------------
                        double PromArt32 = 0;

                        PromArt32 = Convert.ToDouble(CalifDici[7]) + Convert.ToDouble(CalifEner[7]) + Convert.ToDouble(CalifFebr[7]);
                        PromArt32 = PromArt32 / 3;
                        PromTriArt2 = PromArt32.ToString("0.#");//Promedio del primer trimestre de Artes

                        //Asignacion del nivel del primer trimestre de Artes
                        if (Convert.ToDouble(PromTriArt2) >= 5 && Convert.ToDouble(PromTriArt2) < 6)
                        {
                            NivelArt2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt2) >= 6 && Convert.ToDouble(PromTriArt2) < 8)
                            {
                                NivelArt2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt2) >= 8 && Convert.ToDouble(PromTriArt2) < 10)
                                {
                                    NivelArt2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt2) == 10)
                                    {
                                        NivelArt2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelArt2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ed. Fisica-------------------------------------------------
                        double PromFis32 = 0;

                        PromFis32 = Convert.ToDouble(CalifDici[8]) + Convert.ToDouble(CalifEner[8]) + Convert.ToDouble(CalifFebr[8]);
                        PromFis32 = PromFis32 / 3;
                        PromTriFis2 = PromFis32.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                        //Asignacion del nivel del primer trimestre de Ed. Fisica
                        if (Convert.ToDouble(PromTriFis2) >= 5 && Convert.ToDouble(PromTriFis2) < 6)
                        {
                            NivelFis2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis2) >= 6 && Convert.ToDouble(PromTriFis2) < 8)
                            {
                                NivelFis2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis2) >= 8 && Convert.ToDouble(PromTriFis2) < 10)
                                {
                                    NivelFis2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis2) == 10)
                                    {
                                        NivelFis2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelFis2 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                        double PromSoc32 = 0;

                        PromSoc32 = Convert.ToDouble(CalifDici[9]) + Convert.ToDouble(CalifEner[9]) + Convert.ToDouble(CalifFebr[9]);
                        PromSoc32 = PromSoc32 / 3;
                        PromTriSoc2 = PromSoc32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                        //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                        if (Convert.ToDouble(PromTriSoc2) >= 5 && Convert.ToDouble(PromTriSoc2) < 6)
                        {
                            NivelSoc2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc2) >= 6 && Convert.ToDouble(PromTriSoc2) < 8)
                            {
                                NivelSoc2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc2) >= 8 && Convert.ToDouble(PromTriSoc2) < 10)
                                {
                                    NivelSoc2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc2) == 10)
                                    {
                                        NivelSoc2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelSoc2 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Promedio de materias desarrollo social segundo trimestre-------------------------------------------------
                        double PromDes32 = 0;

                        PromDes32 = Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriSoc2);
                        PromDes32 = PromDes32 / 3;
                        PromTriDes2 = PromDes32.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriDes2) >= 5 && Convert.ToDouble(PromTriDes2) < 6)
                        {
                            NivelPromDes2 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes2) >= 6 && Convert.ToDouble(PromTriDes2) < 8)
                            {
                                NivelPromDes2 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes2) >= 8 && Convert.ToDouble(PromTriDes2) < 10)
                                {
                                    NivelPromDes2 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes2) == 10)
                                    {
                                        NivelPromDes2 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromDes2 = " ";
                                    }
                                }
                            }
                        }

                        //-----------------------------Inasistencia-----------------------------------------------------
                        double SumaInasis32 = 0;

                        SumaInasis32 = Convert.ToDouble(CalifDici[10]) + Convert.ToDouble(CalifEner[10]) + Convert.ToDouble(CalifFebr[10]);
                        SumaInasisTri2 = SumaInasis32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifFebr[i] = " ";
                        }
                    }
                    conn6.Close();
                    //Marzo-----------------------------------------------------------------
                    string CalifMar = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Marzo'";

                    MySqlConnection conn7;
                    MySqlCommand com7;

                    conn7 = new MySqlConnection(conexion);
                    conn7.Open();

                    com7 = new MySqlCommand(CalifMar, conn7);

                    MySqlDataReader myreader7 = com7.ExecuteReader();

                    string[] CalifMarz = new string[11];
                    string PromMar = " ";
                    string PromMarzGeneral = " ";

                    if (myreader7.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader7.Read())//Agrega calificaciones
                        {
                            double[] CalifMarztemp = new double[11];
                            CalifMarztemp[L] = Convert.ToDouble(myreader7["CalificacionMen"]);
                            CalifMarz[L] = CalifMarztemp[L].ToString("0.#");
                            L++;
                        }

                        double PromMarTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromMarTemp = PromMarTemp + Convert.ToDouble(CalifMarz[i]);
                        }

                        PromMarTemp = PromMarTemp / 9;
                        PromMar = PromMarTemp.ToString("0.#");//Promedio de  marzo

                        double PromMarzGen = 0;

                        PromMarzGen = Convert.ToDouble(PromMar) + Convert.ToDouble(CalifMarz[9]);
                        PromMarzGen = PromMarzGen / 2;
                        PromMarzGeneral = PromMarzGen.ToString("0.#"); //Promedio general de marzo

                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifMarz[i] = " ";
                        }
                    }
                    conn7.Close();
                    //Abril-----------------------------------------------------------------
                    string CalifAbr = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Abril'";

                    MySqlConnection conn8;
                    MySqlCommand com8;

                    conn8 = new MySqlConnection(conexion);
                    conn8.Open();

                    com8 = new MySqlCommand(CalifAbr, conn8);

                    MySqlDataReader myreader8 = com8.ExecuteReader();

                    string[] CalifAbri = new string[11];
                    string PromAbr = " ";
                    string PromAbriGeneral = " ";

                    if (myreader8.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader8.Read())//Agrega calificaciones
                        {
                            double[] CalifAbritemp = new double[11];
                            CalifAbritemp[L] = Convert.ToDouble(myreader8["CalificacionMen"]);
                            CalifAbri[L] = CalifAbritemp[L].ToString("0.#");
                            L++;
                        }

                        double PromAbrTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromAbrTemp = PromAbrTemp + Convert.ToDouble(CalifAbri[i]);
                        }

                        PromAbrTemp = PromAbrTemp / 9;
                        PromAbr = PromAbrTemp.ToString("0.#");//Promedio de abril

                        double PromAbriGen = 0;

                        PromAbriGen = Convert.ToDouble(PromAbr) + Convert.ToDouble(CalifAbri[9]);
                        PromAbriGen = PromAbriGen / 2;
                        PromAbriGeneral = PromAbriGen.ToString("0.#"); //Promedio general de abril

                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifAbri[i] = " ";
                        }
                    }
                    conn8.Close();
                    //Mayo-----------------------------------------------------------------
                    string CalifMay = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Mayo'";

                    MySqlConnection conn9;
                    MySqlCommand com9;

                    conn9 = new MySqlConnection(conexion);
                    conn9.Open();

                    com9 = new MySqlCommand(CalifMay, conn9);

                    MySqlDataReader myreader9 = com9.ExecuteReader();

                    string[] CalifMayo = new string[11];
                    string PromMay = " ";
                    string PromMayoGeneral = " ";

                    if (myreader9.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader9.Read())//Agrega calificaciones
                        {
                            double[] CalifMayotemp = new double[11];
                            CalifMayotemp[L] = Convert.ToDouble(myreader9["CalificacionMen"]);
                            CalifMayo[L] = CalifMayotemp[L].ToString("0.#");
                            L++;
                        }

                        double PromMayTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromMayTemp = PromMayTemp + Convert.ToDouble(CalifMayo[i]);
                        }

                        PromMayTemp = PromMayTemp / 9;
                        PromMay = PromMayTemp.ToString("0.#");//Promedio de mayo

                        double PromMayoGen = 0;

                        PromMayoGen = Convert.ToDouble(PromMay) + Convert.ToDouble(CalifMayo[9]);
                        PromMayoGen = PromMayoGen / 2;
                        PromMayoGeneral = PromMayoGen.ToString("0.#"); //Promedio general de mayo
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifMayo[i] = " ";
                        }
                    }
                    conn9.Close();
                    //Junio-----------------------------------------------------------------
                    string CalifJun = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Junio'";

                    MySqlConnection conn10;
                    MySqlCommand com10;

                    conn10 = new MySqlConnection(conexion);
                    conn10.Open();

                    com10 = new MySqlCommand(CalifJun, conn10);

                    MySqlDataReader myreader10 = com10.ExecuteReader();

                    string[] CalifJuni = new string[11];
                    string PromJun = " ";
                    string PromTriEsp3 = " ";
                    string PromTriMat3 = " ";
                    string PromTriIng3 = " ";
                    string PromTricn3 = " ";
                    string PromTriGeo3 = " ";
                    string PromTriHis3 = " ";
                    string PromTriCiv3 = " ";
                    string PromTriArt3 = " ";
                    string PromTriFis3 = " ";
                    string PromTriSoc3 = " ";
                    string SumaInasisTri3 = " ";
                    string Nivel3 = " ";
                    string NivelMat3 = " ";
                    string NivelIng3 = " ";
                    string Nivelcn3 = " ";
                    string NivelGeo3 = " ";
                    string NivelHis3 = " ";
                    string NivelCiv3 = " ";
                    string NivelArt3 = " ";
                    string NivelFis3 = " ";
                    string NivelSoc3 = " ";
                    string PromTriGen3 = " ";
                    string NivelGen3 = " ";
                    string PromFinTriEsp = " ";
                    string NivelFin = " ";
                    string PromFinTriMat = " ";
                    string NivelMatFin = " ";
                    string PromFinTriIng = " ";
                    string NivelIngFin = " ";
                    string PromFinTricn = " ";
                    string NivelcnFin = " ";
                    string PromFinTriGeo = " ";
                    string NivelGeoFin = " ";
                    string PromFinTriHis = " ";
                    string NivelHisFin = " ";
                    string PromFinTriCiv = " ";
                    string NivelCivFin = " ";
                    string PromFinTriArt = " ";
                    string NivelArtFin = " ";
                    string PromFinTriFis = " ";
                    string NivelFisFin = " ";
                    string PromFinTriSoc = " ";
                    string NivelSocFin = " ";
                    string PromTriSen3 = " ";
                    string NivelPromSen3 = " ";
                    string PromTriSenFin = " ";
                    string NivelPromSenFin = " ";
                    string PromTriGenFin = " ";
                    string NivelGenFin = " ";
                    string SumInasisTriFin = " ";

                    string PromTriDes3 = " ";
                    string NivelPromDes3 = " ";
                    string PromTriDesFin = " ";
                    string NivelPromDesFin = " ";
                    string PorcAsistencias = " ";

                    string Si = " ";
                    string No = " ";
                    string Yes = " ";
                    string Nou = " ";
                    string Yes1 = " ";
                    string Nou1 = " ";
                    string Yes2 = " ";
                    string Nou2 = " ";
                    string Yes3 = " ";
                    string Nou3 = " ";
                    string Yes4 = " ";
                    string Nou4 = " ";
                    string PromMateriasIC = " ";
                    string SumAsisTriFin = " ";

                    string PromJuniGeneral = " ";

                    if (myreader10.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader10.Read())//Agrega calificaciones
                        {
                            double[] CalifJunitemp = new double[11];
                            CalifJunitemp[L] = Convert.ToDouble(myreader10["CalificacionMen"]);
                            CalifJuni[L] = CalifJunitemp[L].ToString("0.#");
                            L++;
                        }

                        double PromJunTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromJunTemp = PromJunTemp + Convert.ToDouble(CalifJuni[i]);
                        }

                        PromJunTemp = PromJunTemp / 9;
                        PromJun = PromJunTemp.ToString("0.#");//Promedio de junio

                        double PromJuniGen = 0;

                        PromJuniGen = Convert.ToDouble(PromJun) + Convert.ToDouble(CalifJuni[9]);
                        PromJuniGen = PromJuniGen / 2;
                        PromJuniGeneral = PromJuniGen.ToString("0.#"); //Promedio general de junio

                        //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                        double PromSen33 = 0;

                        PromSen33 = Convert.ToDouble(PromMar) + Convert.ToDouble(PromAbr) + Convert.ToDouble(PromMay) + Convert.ToDouble(PromJun);
                        PromSen33 = PromSen33 / 4;
                        PromTriSen3 = PromSen33.ToString("0.#");//Promedio del segundo trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriSen3) >= 5 && Convert.ToDouble(PromTriSen3) < 6)
                        {
                            NivelPromSen3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSen3) >= 6 && Convert.ToDouble(PromTriSen3) < 8)
                            {
                                NivelPromSen3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen3) >= 8 && Convert.ToDouble(PromTriSen3) < 10)
                                {
                                    NivelPromSen3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen3) == 10)
                                    {
                                        NivelPromSen3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromSen3 = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                        double PromGen33 = 0;

                        PromGen33 = Convert.ToDouble(PromMarzGeneral) + Convert.ToDouble(PromAbriGeneral) + Convert.ToDouble(PromMayoGeneral) + Convert.ToDouble(PromJuniGeneral);
                        PromGen33 = PromGen33 / 4;
                        PromTriGen3 = PromGen33.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriGen3) >= 5 && Convert.ToDouble(PromTriGen3) < 6)
                        {
                            NivelGen3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen3) >= 6 && Convert.ToDouble(PromTriGen3) < 8)
                            {
                                NivelGen3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen3) >= 8 && Convert.ToDouble(PromTriGen3) < 10)
                                {
                                    NivelGen3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen3) == 10)
                                    {
                                        NivelGen3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGen3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Español-------------------------------------------------

                        double PromEsp33 = 0;

                        PromEsp33 = Convert.ToDouble(CalifMarz[0]) + Convert.ToDouble(CalifAbri[0]) + Convert.ToDouble(CalifMayo[0]) + Convert.ToDouble(CalifJuni[0]);
                        PromEsp33 = PromEsp33 / 4;
                        PromTriEsp3 = PromEsp33.ToString("0.#");//Promedio del tercer trimestre de español

                        //Asignacion del nivel del tercer trimestre de español
                        if (Convert.ToDouble(PromTriEsp3) >= 5 && Convert.ToDouble(PromTriEsp3) < 6)
                        {
                            Nivel3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriEsp3) >= 6 && Convert.ToDouble(PromTriEsp3) < 8)
                            {
                                Nivel3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp3) >= 8 && Convert.ToDouble(PromTriEsp3) < 10)
                                {
                                    Nivel3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp3) == 10)
                                    {
                                        Nivel3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivel3 = " ";
                                    }
                                }
                            }
                        }

                        //-------------------------------------Matematicas--------------------------------------------------------
                        double PromMat33 = 0;

                        PromMat33 = Convert.ToDouble(CalifMarz[1]) + Convert.ToDouble(CalifAbri[1]) + Convert.ToDouble(CalifMayo[1]) + Convert.ToDouble(CalifJuni[1]);
                        PromMat33 = PromMat33 / 4;
                        PromTriMat3 = PromMat33.ToString("0.#");//Promedio del primer trimestre de matematicas

                        //Asignacion del nivel del primer trimestre de matematicas
                        if (Convert.ToDouble(PromTriMat3) >= 5 && Convert.ToDouble(PromTriMat3) < 6)
                        {
                            NivelMat3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriMat3) >= 6 && Convert.ToDouble(PromTriMat3) < 8)
                            {
                                NivelMat3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat3) >= 8 && Convert.ToDouble(PromTriMat3) < 10)
                                {
                                    NivelMat3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat3) == 10)
                                    {
                                        NivelMat3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelMat3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ingles-------------------------------------------------
                        double PromIng33 = 0;

                        PromIng33 = Convert.ToDouble(CalifMarz[2]) + Convert.ToDouble(CalifAbri[2]) + Convert.ToDouble(CalifMayo[2]) + Convert.ToDouble(CalifJuni[2]);
                        PromIng33 = PromIng33 / 4;
                        PromTriIng3 = PromIng33.ToString("0.#");//Promedio del tercer trimestre de Ingles

                        //Asignacion del nivel del tercer trimestre de Ingles
                        if (Convert.ToDouble(PromTriIng3) >= 5 && Convert.ToDouble(PromTriIng3) < 6)
                        {
                            NivelIng3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriIng3) >= 6 && Convert.ToDouble(PromTriIng3) < 8)
                            {
                                NivelIng3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng3) >= 8 && Convert.ToDouble(PromTriIng3) < 10)
                                {
                                    NivelIng3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng3) == 10)
                                    {
                                        NivelIng3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelIng3 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                        double Promcn33 = 0;

                        Promcn33 = Convert.ToDouble(CalifMarz[3]) + Convert.ToDouble(CalifAbri[3]) + Convert.ToDouble(CalifMayo[3]) + Convert.ToDouble(CalifJuni[3]);
                        Promcn33 = Promcn33 / 4;
                        PromTricn3 = Promcn33.ToString("0.#");//Promedio del tercer trimestre de Ciencias

                        //Asignacion del nivel del tercer trimestre de Ciencias
                        if (Convert.ToDouble(PromTricn3) >= 5 && Convert.ToDouble(PromTricn3) < 6)
                        {
                            Nivelcn3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTricn3) >= 6 && Convert.ToDouble(PromTricn3) < 8)
                            {
                                Nivelcn3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn3) >= 8 && Convert.ToDouble(PromTricn3) < 10)
                                {
                                    Nivelcn3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn3) == 10)
                                    {
                                        Nivelcn3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        Nivelcn3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Geografia-------------------------------------------------
                        double PromGeo33 = 0;

                        PromGeo33 = Convert.ToDouble(CalifMarz[4]) + Convert.ToDouble(CalifAbri[4]) + Convert.ToDouble(CalifMayo[4]) + Convert.ToDouble(CalifJuni[4]);
                        PromGeo33 = PromGeo33 / 4;
                        PromTriGeo3 = PromGeo33.ToString("0.#");//Promedio del tercer trimestre de Geografia

                        //Asignacion del nivel del tercer trimestre de Geografia
                        if (Convert.ToDouble(PromTriGeo3) >= 5 && Convert.ToDouble(PromTriGeo3) < 6)
                        {
                            NivelGeo3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGeo3) >= 6 && Convert.ToDouble(PromTriGeo3) < 8)
                            {
                                NivelGeo3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo3) >= 8 && Convert.ToDouble(PromTriGeo3) < 10)
                                {
                                    NivelGeo3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo3) == 10)
                                    {
                                        NivelGeo3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGeo3 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Historia--------------------------------------------------------
                        double PromHis33 = 0;

                        PromHis33 = Convert.ToDouble(CalifMarz[5]) + Convert.ToDouble(CalifAbri[5]) + Convert.ToDouble(CalifMayo[5]) + Convert.ToDouble(CalifJuni[5]);
                        PromHis33 = PromHis33 / 4;
                        PromTriHis3 = PromHis33.ToString("0.#");//Promedio del tercer trimestre de Historia

                        //Asignacion del nivel del tercer trimestre de Historia
                        if (Convert.ToDouble(PromTriHis3) >= 5 && Convert.ToDouble(PromTriHis3) < 6)
                        {
                            NivelHis3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriHis3) >= 6 && Convert.ToDouble(PromTriHis3) < 8)
                            {
                                NivelHis3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis3) >= 8 && Convert.ToDouble(PromTriHis3) < 10)
                                {
                                    NivelHis3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis3) == 10)
                                    {
                                        NivelHis3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelHis3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Formacion Civica y etica-------------------------------------------------
                        double PromCiv33 = 0;

                        PromCiv33 = Convert.ToDouble(CalifMarz[6]) + Convert.ToDouble(CalifAbri[6]) + Convert.ToDouble(CalifMayo[6]) + Convert.ToDouble(CalifJuni[6]);
                        PromCiv33 = PromCiv33 / 4;
                        PromTriCiv3 = PromCiv33.ToString("0.#");//Promedio del tercer trimestre de Formacion Civica y etica

                        //Asignacion del nivel del tercer trimestre de Formacion Civica y etica
                        if (Convert.ToDouble(PromTriCiv3) >= 5 && Convert.ToDouble(PromTriCiv3) < 6)
                        {
                            NivelCiv3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCiv3) >= 6 && Convert.ToDouble(PromTriCiv3) < 8)
                            {
                                NivelCiv3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv3) >= 8 && Convert.ToDouble(PromTriCiv3) < 10)
                                {
                                    NivelCiv3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv3) == 10)
                                    {
                                        NivelCiv3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelCiv3 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Artes--------------------------------------------------------
                        double PromArt33 = 0;

                        PromArt33 = Convert.ToDouble(CalifMarz[7]) + Convert.ToDouble(CalifAbri[7]) + Convert.ToDouble(CalifMayo[7]) + Convert.ToDouble(CalifJuni[7]);
                        PromArt33 = PromArt33 / 4;
                        PromTriArt3 = PromArt33.ToString("0.#");//Promedio del tercer trimestre de Artes

                        //Asignacion del nivel del tercer trimestre de Artes
                        if (Convert.ToDouble(PromTriArt3) >= 5 && Convert.ToDouble(PromTriArt3) < 6)
                        {
                            NivelArt3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriArt3) >= 6 && Convert.ToDouble(PromTriArt3) < 8)
                            {
                                NivelArt3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt3) >= 8 && Convert.ToDouble(PromTriArt3) < 10)
                                {
                                    NivelArt3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt3) == 10)
                                    {
                                        NivelArt3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelArt3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Ed. Fisica-------------------------------------------------
                        double PromFis33 = 0;

                        PromFis33 = Convert.ToDouble(CalifMarz[8]) + Convert.ToDouble(CalifAbri[8]) + Convert.ToDouble(CalifMayo[8]) + Convert.ToDouble(CalifJuni[8]);
                        PromFis33 = PromFis33 / 4;
                        PromTriFis3 = PromFis33.ToString("0.#");//Promedio del tercer trimestre de Ed. Fisica

                        //Asignacion del nivel del tercer trimestre de Ed. Fisica
                        if (Convert.ToDouble(PromTriFis3) >= 5 && Convert.ToDouble(PromTriFis3) < 6)
                        {
                            NivelFis3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriFis3) >= 6 && Convert.ToDouble(PromTriFis3) < 8)
                            {
                                NivelFis3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis3) >= 8 && Convert.ToDouble(PromTriFis3) < 10)
                                {
                                    NivelFis3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis3) == 10)
                                    {
                                        NivelFis3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelFis3 = " ";
                                    }
                                }
                            }
                        }
                        //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                        double PromSoc33 = 0;

                        PromSoc33 = Convert.ToDouble(CalifMarz[9]) + Convert.ToDouble(CalifAbri[9]) + Convert.ToDouble(CalifMayo[9]) + Convert.ToDouble(CalifJuni[9]);
                        PromSoc33 = PromSoc33 / 4;
                        PromTriSoc3 = PromSoc33.ToString("0.#");//Promedio del tercer trimestre de Ed. Socioemocional

                        //Asignacion del nivel del tercer trimestre de Ed. Socioemocional
                        if (Convert.ToDouble(PromTriSoc3) >= 5 && Convert.ToDouble(PromTriSoc3) < 6)
                        {
                            NivelSoc3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSoc3) >= 6 && Convert.ToDouble(PromTriSoc3) < 8)
                            {
                                NivelSoc3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc3) >= 8 && Convert.ToDouble(PromTriSoc3) < 10)
                                {
                                    NivelSoc3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc3) == 10)
                                    {
                                        NivelSoc3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelSoc3 = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                        double PromDes33 = 0;

                        PromDes33 = Convert.ToDouble(PromTriArt3) + Convert.ToDouble(PromTriFis3) + Convert.ToDouble(PromTriSoc3);
                        PromDes33 = PromDes33 / 3;
                        PromTriDes3 = PromDes33.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriDes3) >= 5 && Convert.ToDouble(PromTriDes3) < 6)
                        {
                            NivelPromDes3 = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDes3) >= 6 && Convert.ToDouble(PromTriDes3) < 8)
                            {
                                NivelPromDes3 = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes3) >= 8 && Convert.ToDouble(PromTriDes3) < 10)
                                {
                                    NivelPromDes3 = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes3) == 10)
                                    {
                                        NivelPromDes3 = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromDes3 = " ";
                                    }
                                }
                            }
                        }

                        //-----------------------------Inasistencia-----------------------------------------------------
                        double SumaInasis33 = 0;

                        SumaInasis33 = Convert.ToDouble(CalifMarz[10]) + Convert.ToDouble(CalifAbri[10]) + Convert.ToDouble(CalifMayo[10]) + Convert.ToDouble(CalifJuni[10]);
                        SumaInasisTri3 = SumaInasis33.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                        //------------------------------------Promedio de materias sencillas final trimestres-------------------------------------------------
                        double PromSenFin = 0;

                        PromSenFin = Convert.ToDouble(PromTriSen1) + Convert.ToDouble(PromTriSen2) + Convert.ToDouble(PromTriSen3);
                        PromSenFin = PromSenFin / 3;
                        PromTriSenFin = PromSenFin.ToString("0.#");//Promedio del segundo trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriSenFin) >= 5 && Convert.ToDouble(PromTriSenFin) < 6)
                        {
                            NivelPromSenFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriSenFin) >= 6 && Convert.ToDouble(PromTriSenFin) < 8)
                            {
                                NivelPromSenFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSenFin) >= 8 && Convert.ToDouble(PromTriSenFin) < 10)
                                {
                                    NivelPromSenFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSenFin) == 10)
                                    {
                                        NivelPromSenFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromSenFin = " ";
                                    }
                                }
                            }
                        }

                        //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                        double PromGen3Fin = 0;

                        PromGen3Fin = Convert.ToDouble(PromTriGen1) + Convert.ToDouble(PromTriGen2) + Convert.ToDouble(PromTriGen3);
                        PromGen3Fin = PromGen3Fin / 3;
                        PromTriGenFin = PromGen3Fin.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriGenFin) >= 5 && Convert.ToDouble(PromTriGenFin) < 6)
                        {
                            NivelGenFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGenFin) >= 6 && Convert.ToDouble(PromTriGenFin) < 8)
                            {
                                NivelGenFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGenFin) >= 8 && Convert.ToDouble(PromTriGenFin) < 10)
                                {
                                    NivelGenFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGenFin) == 10)
                                    {
                                        NivelGenFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGenFin = " ";
                                    }
                                }
                            }
                        }

                        if (PromGen3Fin >= 6)
                        {
                            Si = "X";
                            No = " ";
                        }
                        else
                        {
                            Si = " ";
                            No = "X";
                        }
                        //--------------------------------------------Nivel general mayor a II ---------------------------------
                        if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                        {
                            Yes4 = "Yes";
                            Nou4 = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                            {
                                Yes4 = "Yes";
                                Nou4 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                                {
                                    Yes4 = "Yes";
                                    Nou4 = "Off";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen3) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                                    {
                                        Yes4 = "Yes";
                                        Nou4 = "Off";
                                    }
                                    else
                                    {
                                        Yes4 = "Off";
                                        Nou4 = "Yes";
                                    }
                                }
                            }
                        }

                        //--------------------------------------Promedio final de español trimestral--------------------------------
                        double PromFin = 0;

                        PromFin = Convert.ToDouble(PromTriEsp1) + Convert.ToDouble(PromTriEsp2) + Convert.ToDouble(PromTriEsp3);
                        PromFin = PromFin / 3;
                        PromFinTriEsp = PromFin.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriEsp) >= 5 && Convert.ToDouble(PromFinTriEsp) < 6)
                        {
                            NivelFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriEsp) >= 6 && Convert.ToDouble(PromFinTriEsp) < 8)
                            {
                                NivelFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriEsp) >= 8 && Convert.ToDouble(PromFinTriEsp) < 10)
                                {
                                    NivelFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriEsp) == 10)
                                    {
                                        NivelFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelFin = " ";
                                    }
                                }
                            }
                        }

                        if (Convert.ToDouble(PromFinTriEsp) > 5.9 && Convert.ToDouble(PromFinTriEsp) <= 10)
                        {
                            Yes = "Yes";
                            Nou = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriEsp) <= 5.9)
                            {
                                Yes = "Off";
                                Nou = "Yes";
                            }
                        }
                        //--------------------------------------Promedio final de matematicas trimestral--------------------------------
                        double PromFinMat = 0;

                        PromFinMat = Convert.ToDouble(PromTriMat1) + Convert.ToDouble(PromTriMat2) + Convert.ToDouble(PromTriMat3);
                        PromFinMat = PromFinMat / 3;
                        PromFinTriMat = PromFinMat.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriMat) >= 5 && Convert.ToDouble(PromFinTriMat) < 6)
                        {
                            NivelMatFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriMat) >= 6 && Convert.ToDouble(PromFinTriMat) < 8)
                            {
                                NivelMatFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriMat) >= 8 && Convert.ToDouble(PromFinTriMat) < 10)
                                {
                                    NivelMatFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriMat) == 10)
                                    {
                                        NivelMatFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelMatFin = " ";
                                    }
                                }
                            }
                        }

                        if (Convert.ToDouble(PromFinTriMat) > 5.9 && Convert.ToDouble(PromFinTriMat) <= 10)
                        {
                            Yes1 = "Yes";
                            Nou1 = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriMat) <= 5.9)
                            {
                                Yes1 = "Off";
                                Nou1 = "Yes";
                            }
                        }
                        //--------------------------------------Promedio final de Ingles trimestral--------------------------------
                        double PromFinIng = 0;

                        PromFinIng = Convert.ToDouble(PromTriIng1) + Convert.ToDouble(PromTriIng2) + Convert.ToDouble(PromTriIng3);
                        PromFinIng = PromFinIng / 3;
                        PromFinTriIng = PromFinIng.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriIng) >= 5 && Convert.ToDouble(PromFinTriIng) < 6)
                        {
                            NivelIngFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriIng) >= 6 && Convert.ToDouble(PromFinTriIng) < 8)
                            {
                                NivelIngFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriIng) >= 8 && Convert.ToDouble(PromFinTriIng) < 10)
                                {
                                    NivelIngFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriIng) == 10)
                                    {
                                        NivelIngFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelIngFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Ciencias Naturales trimestral--------------------------------
                        double PromFincn = 0;

                        PromFincn = Convert.ToDouble(PromTricn1) + Convert.ToDouble(PromTricn2) + Convert.ToDouble(PromTricn3);
                        PromFincn = PromFincn / 3;
                        PromFinTricn = PromFincn.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTricn) >= 5 && Convert.ToDouble(PromFinTricn) < 6)
                        {
                            NivelcnFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTricn) >= 6 && Convert.ToDouble(PromFinTricn) < 8)
                            {
                                NivelcnFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTricn) >= 8 && Convert.ToDouble(PromFinTricn) < 10)
                                {
                                    NivelcnFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTricn) == 10)
                                    {
                                        NivelcnFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelcnFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Geografia trimestral--------------------------------
                        double PromFinGeo = 0;

                        PromFinGeo = Convert.ToDouble(PromTriGeo1) + Convert.ToDouble(PromTriGeo2) + Convert.ToDouble(PromTriGeo3);
                        PromFinGeo = PromFinGeo / 3;
                        PromFinTriGeo = PromFinGeo.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriGeo) >= 5 && Convert.ToDouble(PromFinTriGeo) < 6)
                        {
                            NivelGeoFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriGeo) >= 6 && Convert.ToDouble(PromFinTriGeo) < 8)
                            {
                                NivelGeoFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriGeo) >= 8 && Convert.ToDouble(PromFinTriGeo) < 10)
                                {
                                    NivelGeoFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriGeo) == 10)
                                    {
                                        NivelGeoFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelGeoFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Historia trimestral--------------------------------
                        double PromFinHis = 0;

                        PromFinHis = Convert.ToDouble(PromTriHis1) + Convert.ToDouble(PromTriHis2) + Convert.ToDouble(PromTriHis3);
                        PromFinHis = PromFinHis / 3;
                        PromFinTriHis = PromFinHis.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriHis) >= 5 && Convert.ToDouble(PromFinTriHis) < 6)
                        {
                            NivelHisFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriHis) >= 6 && Convert.ToDouble(PromFinTriHis) < 8)
                            {
                                NivelHisFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriHis) >= 8 && Convert.ToDouble(PromFinTriHis) < 10)
                                {
                                    NivelHisFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriHis) == 10)
                                    {
                                        NivelHisFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelHisFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Formacion Civica y etica trimestral--------------------------------
                        double PromFinCiv = 0;

                        PromFinCiv = Convert.ToDouble(PromTriCiv1) + Convert.ToDouble(PromTriCiv2) + Convert.ToDouble(PromTriCiv3);
                        PromFinCiv = PromFinCiv / 3;
                        PromFinTriCiv = PromFinCiv.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriCiv) >= 5 && Convert.ToDouble(PromFinTriCiv) < 6)
                        {
                            NivelCivFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriCiv) >= 6 && Convert.ToDouble(PromFinTriCiv) < 8)
                            {
                                NivelCivFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriCiv) >= 8 && Convert.ToDouble(PromFinTriCiv) < 10)
                                {
                                    NivelCivFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriCiv) == 10)
                                    {
                                        NivelCivFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelCivFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Artes trimestral--------------------------------
                        double PromFinArt = 0;

                        PromFinArt = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriArt3);
                        PromFinArt = PromFinArt / 3;
                        PromFinTriArt = PromFinArt.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriArt) >= 5 && Convert.ToDouble(PromFinTriArt) < 6)
                        {
                            NivelArtFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriArt) >= 6 && Convert.ToDouble(PromFinTriArt) < 8)
                            {
                                NivelArtFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriArt) >= 8 && Convert.ToDouble(PromFinTriArt) < 10)
                                {
                                    NivelArtFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriArt) == 10)
                                    {
                                        NivelArtFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelArtFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Ed. Fisica trimestral--------------------------------
                        double PromFinFis = 0;

                        PromFinFis = Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriFis3);
                        PromFinFis = PromFinFis / 3;
                        PromFinTriFis = PromFinFis.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriFis) >= 5 && Convert.ToDouble(PromFinTriFis) < 6)
                        {
                            NivelFisFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriFis) >= 6 && Convert.ToDouble(PromFinTriFis) < 8)
                            {
                                NivelFisFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriFis) >= 8 && Convert.ToDouble(PromFinTriFis) < 10)
                                {
                                    NivelFisFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriFis) == 10)
                                    {
                                        NivelFisFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelFisFin = " ";
                                    }
                                }
                            }
                        }
                        //--------------------------------------Promedio final de Ed. Socioemocional trimestral--------------------------------
                        double PromFinSoc = 0;

                        PromFinSoc = Convert.ToDouble(PromTriSoc1) + Convert.ToDouble(PromTriSoc2) + Convert.ToDouble(PromTriSoc3);
                        PromFinSoc = PromFinSoc / 3;
                        PromFinTriSoc = PromFinSoc.ToString("0.#");//Promedio final de los trimestres de español

                        //Asignacion del nivel de los trimestres de español
                        if (Convert.ToDouble(PromFinTriSoc) >= 5 && Convert.ToDouble(PromFinTriSoc) < 6)
                        {
                            NivelSocFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriSoc) >= 6 && Convert.ToDouble(PromFinTriSoc) < 8)
                            {
                                NivelSocFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriSoc) >= 8 && Convert.ToDouble(PromFinTriSoc) < 10)
                                {
                                    NivelSocFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriSoc) == 10)
                                    {
                                        NivelSocFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelSocFin = " ";
                                    }
                                }
                            }
                        }
                        //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                        double PromDesFin = 0;

                        PromDesFin = Convert.ToDouble(PromFinTriArt) + Convert.ToDouble(PromFinTriFis) + Convert.ToDouble(PromFinTriSoc);
                        PromDesFin = PromDesFin / 3;
                        PromTriDesFin = PromDesFin.ToString("0.#");//Promedio del primer trimestre 

                        //Asignacion del nivel del primer trimestre 
                        if (Convert.ToDouble(PromTriDesFin) >= 5 && Convert.ToDouble(PromTriDesFin) < 6)
                        {
                            NivelPromDesFin = "I";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriDesFin) >= 6 && Convert.ToDouble(PromTriDesFin) < 8)
                            {
                                NivelPromDesFin = "II";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDesFin) >= 8 && Convert.ToDouble(PromTriDesFin) < 10)
                                {
                                    NivelPromDesFin = "III";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDesFin) == 10)
                                    {
                                        NivelPromDesFin = "IV";
                                    }
                                    else
                                    {
                                        MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                        NivelPromDesFin = " ";
                                    }
                                }
                            }
                        }

                        //-----------------------------si el nivel es mas alto en el desarrollo -------------------------------
                        if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                        {
                            Yes3 = "Yes";
                            Nou3 = "Off";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                            {
                                Yes3 = "Yes";
                                Nou3 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                                {
                                    Yes3 = "Yes";
                                    Nou3 = "Off";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriSoc) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                                    {
                                        Yes3 = "Yes";
                                        Nou3 = "Off";
                                    }
                                    else
                                    {
                                        Yes3 = "Off";
                                        Nou3 = "Yes";
                                    }
                                }
                            }
                        }
                        //--------------------Promedio de materias de formacion de ingles a conocimiento -------------------------------------------------
                        double PromMaterias = 0;

                        PromMaterias = Convert.ToDouble(PromFinTriIng) + Convert.ToDouble(PromFinTricn) + Convert.ToDouble(PromFinTriGeo) + Convert.ToDouble(PromFinTriHis) + Convert.ToDouble(PromFinTriCiv);
                        PromMaterias = PromMaterias / 5;
                        PromMateriasIC = PromMaterias.ToString("0.#");//Promedio del primer trimestre 

                        if (Convert.ToDouble(PromMateriasIC) >= 6)
                        {
                            Yes2 = "Yes";
                            Nou2 = "Off";
                        }
                        else
                        {
                            Yes2 = "Off";
                            Nou2 = "Yes";
                        }
                        //-----------------------------Numero final de Inasistencia-----------------------------------------------------
                        double SumaInasisFin = 0;
                        double SumaAsist = 0;
                        double PorcAsis = 0;

                        SumaInasisFin = Convert.ToDouble(SumaInasisTri1) + Convert.ToDouble(SumaInasisTri2) + Convert.ToDouble(SumaInasisTri3);
                        SumInasisTriFin = SumaInasisFin.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                        SumaAsist = 185 - SumaInasisFin;
                        SumAsisTriFin = SumaAsist.ToString("0.#");

                        PorcAsis = SumaAsist / 185;
                        PorcAsis = PorcAsis * 100;
                        PorcAsistencias = PorcAsis.ToString("0.##");
                    }
                    else
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            CalifJuni[i] = " ";
                        }
                    }
                    conn10.Close();

                    //Diagnostico-----------------------------------------------------------------
                    string CalifDig = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diagnostico'";

                    MySqlConnection conn11;
                    MySqlCommand com11;

                    conn11 = new MySqlConnection(conexion);
                    conn11.Open();

                    com11 = new MySqlCommand(CalifDig, conn11);

                    MySqlDataReader myreader11 = com11.ExecuteReader();

                    string[] CalifDiag = new string[11];
                    string PromDiag = " ";
                    string PromDiagGeneral = " ";

                    if (myreader11.HasRows) //Checa si las celdas estan vacias
                    {
                        int L = 0;
                        while (myreader11.Read())//Agrega calificaciones
                        {
                            double[] CalifDiagtemp = new double[11];
                            CalifDiagtemp[L] = Convert.ToDouble(myreader11["CalificacionMen"]);
                            CalifDiag[L] = CalifDiagtemp[L].ToString("0.#");
                            L++;
                        }

                        double PromDiagTemp = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            PromDiagTemp = PromDiagTemp + Convert.ToDouble(CalifDiag[i]);
                        }

                        PromDiagTemp = PromDiagTemp / 9;
                        PromDiag = PromDiagTemp.ToString("0.#");//Promedio de diagnostico

                        double PromDiagGen = 0;

                        PromDiagGen = Convert.ToDouble(PromDiag) + Convert.ToDouble(CalifDiag[9]);
                        PromDiagGen = PromDiagGen / 2;
                        PromDiagGeneral = PromDiagGen.ToString("0.#"); //Promedio general de diagnostico
                    }
                    else
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            CalifDiag[i] = " ";
                        }
                    }
                    conn11.Close();
                    //----------------------------------------------------------------------------------------------
                    string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                    if (!Directory.Exists(folderPath))// pregunt si no existe
                    {
                        Directory.CreateDirectory(folderPath); // si no existe lo crea
                    }
                    string inputFile = Path.Combine(folderPath, "5_PRIMARIA_1819.pdf");
                    string outputFile = Path.Combine(folderPath, "BoletaExterna5.pdf");

                    PdfReader pdfReader = new PdfReader(inputFile);
                    using (FileStream fs = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (PdfStamper stamper = new PdfStamper(pdfReader, fs))
                        {
                            AcroFields fields = stamper.AcroFields;
                            //fields.SetFieldProperty("PRIMER APELLIDO", "fontsize", 14, null);
                            fields.SetField("PRIMER APELLIDO", Apellidop1);
                            fields.SetField("SEGUNDO APELLIDO", Apellidom1);
                            fields.SetField("NOMBRE(S)", nombre1);
                            fields.SetField("CURP", sesion.Curp);
                            fields.SetField("NOMBRE DE LA ESCUELA", "INSTITUTO RODOLFO NERI VELA");
                            fields.SetField("GRUPO", "A");
                            fields.SetField("TURNO", "MATUTINO");
                            fields.SetField("CCT", "12DPT0003N");


                            fields.SetField("Calendario Escolar", "2018- 2019");
                            fields.SetField("Asistencias", SumAsisTriFin);
                            fields.SetField("Inasistencias", SumInasisTriFin);
                            fields.SetField("CRITERIO DE ACREDITACIÓN % Asistencia", PorcAsistencias, " %");

                            //Si aprobo o no
                            fields.SetField("TextField", Si);
                            fields.SetField("TextField_1", No);

                            fields.SetField("PROMEDIO FINAL DEL NIVELDE DESEMPEÑO DEL GRADO", PromTriGenFin);

                            //ESPAÑOL
                            fields.SetField("Nivel de desempeño", Nivel1);
                            fields.SetField("Nivel de desempeño_1", Nivel2);
                            fields.SetField("Nivel de desempeño_2", Nivel3);
                            fields.SetField("Nivel de desempeño_3", NivelFin);

                            fields.SetField("Calificación", PromTriEsp1);
                            fields.SetField("Calificación_1", PromTriEsp2);
                            fields.SetField("Calificación_2", PromTriEsp3);
                            fields.SetField("Calificación_3", PromFinTriEsp);

                            //Check box
                            fields.SetField("CheckBox", Yes);
                            fields.SetField("CheckBox_1", Nou);

                            //MATEMATICAS
                            fields.SetField("Nivel de desempeño_4", NivelMat1);
                            fields.SetField("Nivel de desempeño_5", NivelMat2);
                            fields.SetField("Nivel de desempeño_6", NivelMat3);
                            fields.SetField("Nivel de desempeño_7", NivelMatFin);

                            fields.SetField("Calificación_4", PromTriMat1);
                            fields.SetField("Calificación_5", PromTriMat2);
                            fields.SetField("Calificación_6", PromTriMat3);
                            fields.SetField("Calificación_7", PromFinTriMat);

                            //Check box
                            fields.SetField("CheckBox_2", Yes1);
                            fields.SetField("CheckBox_3", Nou1);

                            //INGLES
                            fields.SetField("Nivel de desempeño_8", NivelIng1);
                            fields.SetField("Nivel de desempeño_9", NivelIng2);
                            fields.SetField("Nivel de desempeño_10", NivelIng3);
                            fields.SetField("Nivel de desempeño_11", NivelIngFin);

                            fields.SetField("Calificación_8", PromTriIng1);
                            fields.SetField("Calificación_9", PromTriIng2);
                            fields.SetField("Calificación_10", PromTriIng3);
                            fields.SetField("Calificación_11", PromFinTriIng);

                            //CIENCIAS NATURALES
                            fields.SetField("Nivel de desempeño_12", Nivelcn1);
                            fields.SetField("Nivel de desempeño_13", Nivelcn2);
                            fields.SetField("Nivel de desempeño_14", Nivelcn3);
                            fields.SetField("Nivel de desempeño_15", NivelcnFin);

                            fields.SetField("Calificación_12", PromTricn1);
                            fields.SetField("Calificación_13", PromTricn2);
                            fields.SetField("Calificación_14", PromTricn3);
                            fields.SetField("Calificación_15", PromFinTricn);

                            //GEOGRAFIA
                            fields.SetField("Nivel de desempeño_16", NivelGeo1);
                            fields.SetField("Nivel de desempeño_17", NivelGeo2);
                            fields.SetField("Nivel de desempeño_18", NivelGeo3);
                            fields.SetField("Nivel de desempeño_19", NivelGeoFin);

                            fields.SetField("Calificación_16", PromTriGeo1);
                            fields.SetField("Calificación_17", PromTriGeo2);
                            fields.SetField("Calificación_18", PromTriGeo3);
                            fields.SetField("Calificación_19", PromFinTriGeo);

                            //HISTORIA
                            fields.SetField("Nivel de desempeño_20", NivelHis1);
                            fields.SetField("Nivel de desempeño_21", NivelHis2);
                            fields.SetField("Nivel de desempeño_22", NivelHis3);
                            fields.SetField("Nivel de desempeño_23", NivelHisFin);

                            fields.SetField("Calificación_20", PromTriHis1);
                            fields.SetField("Calificación_21", PromTriHis2);
                            fields.SetField("Calificación_22", PromTriHis3);
                            fields.SetField("Calificación_23", PromFinTriHis);

                            //FORMACION CIVICA Y ETICA
                            fields.SetField("Nivel de desempeño_24", NivelCiv1);
                            fields.SetField("Nivel de desempeño_25", NivelCiv2);
                            fields.SetField("Nivel de desempeño_26", NivelCiv3);
                            fields.SetField("Nivel de desempeño_27", NivelCivFin);

                            fields.SetField("Calificación_24", PromTriCiv1);
                            fields.SetField("Calificación_25", PromTriCiv2);
                            fields.SetField("Calificación_26", PromTriCiv3);
                            fields.SetField("Calificación_27", PromFinTriCiv);

                            //Check box
                            fields.SetField("CheckBox_4", Yes2);
                            fields.SetField("CheckBox_5", Nou2);


                            fields.SetField("TextField_2", PromMateriasIC);

                            //Promedio formacion academica 
                            fields.SetField("Nivel de desempeño_28", NivelPromSen);
                            fields.SetField("Nivel de desempeño_29", NivelPromSen2);
                            fields.SetField("Nivel de desempeño_30", NivelPromSen3);
                            fields.SetField("Nivel de desempeño_31", NivelPromSenFin);

                            fields.SetField("Calificación_28", PromTriSen1);
                            fields.SetField("Calificación_29", PromTriSen2);
                            fields.SetField("Calificación_30", PromTriSen3);
                            fields.SetField("Calificación_31", PromFinTriMat);

                            //ARTES
                            fields.SetField("ARTES", NivelArt1);
                            fields.SetField("ARTES_1", NivelArt2);
                            fields.SetField("ARTES_2", NivelArt3);
                            fields.SetField("ARTES_3", NivelArtFin);

                            //EDUCACION SOCIOEMOCIONAL 
                            fields.SetField("EDUCACIÓNSOCIOEMOCIONAL", NivelSoc1);
                            fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_1", NivelSoc2);
                            fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_2", NivelSoc3);
                            fields.SetField("EDUCACIÓNSOCIOEMOCIONAL_3", NivelSocFin);

                            //EDUCACION FISICA
                            fields.SetField("EDUCACIÓN FÍSICA", NivelFis1);
                            fields.SetField("EDUCACIÓN FÍSICA_1", NivelFis2);
                            fields.SetField("EDUCACIÓN FÍSICA_2", NivelFis3);
                            fields.SetField("EDUCACIÓN FÍSICA_3", NivelFisFin);

                            //Check box
                            fields.SetField("CheckBox_6", Yes3);
                            fields.SetField("CheckBox_7", Nou3);

                            //Promedio desarrollo social 
                            fields.SetField("PROMEDIOFINAL", NivelPromDes);
                            fields.SetField("PROMEDIOFINAL_1", NivelPromDes2);
                            fields.SetField("PROMEDIOFINAL_2", NivelPromDes3);
                            fields.SetField("PROMEDIOFINAL_3", NivelPromDesFin);

                            //Promedio general
                            fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL", NivelGen1);
                            fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_1", NivelGen2);
                            fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_2", NivelGen3);
                            fields.SetField("AUTONOMÍACURRICULARPROMEDIO FINAL_3", NivelGenFin);

                            //Check box
                            fields.SetField("CheckBox_8", Yes4);
                            fields.SetField("CheckBox_9", Nou4);

                            fields.SetField("LUGAR DE EXPEDICIÓN", "Acapulco, Gro.");
                            fields.SetField("AÑO", "2019");
                            fields.SetField("TextField_4", " ");
                            fields.SetField("DÍA", " ");

                            stamper.Close();
                        }
                    }
                    pdfReader.Close();

                    MessageBox.Show("¡PDF creado!");
                }
                else
                {
                    if (Convert.ToInt32(sesion.grado) == 6)
                    {
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
                        string nombre1, Apellidop1, Apellidom1, IdAlumno1;

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        myreader.Read();
                        nombre1 = Convert.ToString(myreader["nombre"]);
                        Apellidop1 = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom1 = Convert.ToString(myreader["ApellidoM"]);
                        sesion.grado = Convert.ToString(myreader["idGrado"]);
                        IdAlumno1 = Convert.ToString(myreader["idAlumno"]);
                        conn.Close();

                        //-------------------------------Ingresar las calificaciones mensuales de los alumnos---------------------

                        //Septiembre------------------------------------------
                        string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Septiembre'";

                        MySqlConnection conn1;
                        MySqlCommand com1;

                        conn1 = new MySqlConnection(conexion);
                        conn1.Open();

                        com1 = new MySqlCommand(CalifSep, conn1);

                        MySqlDataReader myreader1 = com1.ExecuteReader();

                        string[] CalifSept = new string[11];
                        string PromSept = " ";
                        string PromSeptGeneral = " ";

                        if (myreader1.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader1.Read())//Agrega calificaciones
                            {
                                double[] CalifSeptemp = new double[11];
                                CalifSeptemp[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                                CalifSept[L] = CalifSeptemp[L].ToString("0.#");
                                L++;
                            }

                            double PromSeptTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromSeptTemp = PromSeptTemp + Convert.ToDouble(CalifSept[i]);
                            }

                            PromSeptTemp = PromSeptTemp / 9;
                            PromSept = PromSeptTemp.ToString("0.#"); //Promedio de septiembre

                            double PromSeptGen = 0;

                            PromSeptGen = Convert.ToDouble(PromSept) + Convert.ToDouble(CalifSept[9]);
                            PromSeptGen = PromSeptGen / 2;
                            PromSeptGeneral = PromSeptGen.ToString("0.#"); //Promedio general de semptiembre
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifSept[i] = " ";
                            }
                        }
                        conn1.Close();

                        //Octubre-----------------------------------------------------------------
                        string CalifOct = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Octubre'";

                        MySqlConnection conn2;
                        MySqlCommand com2;

                        conn2 = new MySqlConnection(conexion);
                        conn2.Open();

                        com2 = new MySqlCommand(CalifOct, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        string[] CalifOctu = new string[11];
                        string PromOct = " ";
                        string PromOctuGeneral = " ";

                        if (myreader2.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader2.Read())//Agrega calificaciones
                            {
                                double[] CalifOctutemp = new double[11];
                                CalifOctutemp[L] = Convert.ToDouble(myreader2["CalificacionMen"]);
                                CalifOctu[L] = CalifOctutemp[L].ToString("0.#");
                                L++;
                            }

                            double PromOctTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromOctTemp = PromOctTemp + Convert.ToDouble(CalifOctu[i]);
                            }

                            PromOctTemp = PromOctTemp / 9;
                            PromOct = PromOctTemp.ToString("0.#"); //Promedio de octubre

                            double PromOctGen = 0;

                            PromOctGen = Convert.ToDouble(PromOct) + Convert.ToDouble(CalifOctu[9]);
                            PromOctGen = PromOctGen / 2;
                            PromOctuGeneral = PromOctGen.ToString("0.#"); //Promedio general de semptiembre
                        }
                        else
                        {

                            for (int i = 0; i < 11; i++)
                            {
                                CalifOctu[i] = " ";
                            }
                        }
                        conn2.Close();
                        //Noviembre-----------------------------------------------------------------
                        string CalifNov = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Noviembre'";

                        MySqlConnection conn3;
                        MySqlCommand com3;

                        conn3 = new MySqlConnection(conexion);
                        conn3.Open();

                        com3 = new MySqlCommand(CalifNov, conn3);

                        MySqlDataReader myreader3 = com3.ExecuteReader();

                        string[] CalifNovi = new string[11];
                        string PromNov = " ";
                        string PromTriSen1 = " ";
                        string PromTriEsp1 = " ";
                        string PromTriMat1 = " ";
                        string PromTriIng1 = " ";
                        string PromTricn1 = " ";
                        string PromTriGeo1 = " ";
                        string PromTriHis1 = " ";
                        string PromTriCiv1 = " ";
                        string PromTriArt1 = " ";
                        string PromTriFis1 = " ";
                        string PromTriSoc1 = " ";
                        string SumaInasisTri1 = " ";
                        string NivelPromSen = " ";
                        string Nivel1 = " ";
                        string NivelMat1 = " ";
                        string NivelIng1 = " ";
                        string Nivelcn1 = " ";
                        string NivelGeo1 = " ";
                        string NivelHis1 = " ";
                        string NivelCiv1 = " ";
                        string NivelArt1 = " ";
                        string NivelFis1 = " ";
                        string NivelSoc1 = " ";
                        string PromNoviGeneral = " ";
                        string PromTriGen1 = " ";
                        string NivelGen1 = " ";

                        string NivelPromDes = " ";
                        string PromTriDes1 = " ";

                        if (myreader3.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader3.Read())//Agrega calificaciones
                            {
                                double[] CalifNovitemp = new double[11];
                                CalifNovitemp[L] = Convert.ToDouble(myreader3["CalificacionMen"]);
                                CalifNovi[L] = CalifNovitemp[L].ToString("0.#");
                                L++;
                            }

                            double PromNovTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromNovTemp = PromNovTemp + Convert.ToDouble(CalifNovi[i]);
                            }

                            PromNovTemp = PromNovTemp / 9;
                            PromNov = PromNovTemp.ToString("0.#");//Promedio de noviembre

                            double PromNovGen = 0;

                            PromNovGen = Convert.ToDouble(PromNov) + Convert.ToDouble(CalifNovi[9]);
                            PromNovGen = PromNovGen / 2;
                            PromNoviGeneral = PromNovGen.ToString("0.#"); //Promedio general de nociembre

                            //------------------------------------Promedio de materias sencillas primer trimestre-------------------------------------------------
                            double PromSen31 = 0;

                            PromSen31 = Convert.ToDouble(PromSept) + Convert.ToDouble(PromOct) + Convert.ToDouble(PromNov);
                            PromSen31 = PromSen31 / 3;
                            PromTriSen1 = PromSen31.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                            {
                                NivelPromSen = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                                {
                                    NivelPromSen = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                                    {
                                        NivelPromSen = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSen1) == 10)
                                        {
                                            NivelPromSen = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromSen = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Promedio General primer trimestre-------------------------------------------------
                            double PromGen31 = 0;

                            PromGen31 = Convert.ToDouble(PromSeptGeneral) + Convert.ToDouble(PromOctuGeneral) + Convert.ToDouble(PromNoviGeneral);
                            PromGen31 = PromGen31 / 3;
                            PromTriGen1 = PromGen31.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriGen1) >= 5 && Convert.ToDouble(PromTriGen1) < 6)
                            {
                                NivelGen1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen1) >= 6 && Convert.ToDouble(PromTriGen1) < 8)
                                {
                                    NivelGen1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen1) >= 8 && Convert.ToDouble(PromTriGen1) < 10)
                                    {
                                        NivelGen1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGen1) == 10)
                                        {
                                            NivelGen1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGen1 = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Español-------------------------------------------------
                            double PromEsp31 = 0;

                            PromEsp31 = Convert.ToDouble(CalifSept[0]) + Convert.ToDouble(CalifOctu[0]) + Convert.ToDouble(CalifNovi[0]);
                            PromEsp31 = PromEsp31 / 3;
                            PromTriEsp1 = PromEsp31.ToString("0.#");//Promedio del primer trimestre de español

                            //Asignacion del nivel del primer trimestre de español
                            if (Convert.ToDouble(PromTriEsp1) >= 5 && Convert.ToDouble(PromTriEsp1) < 6)
                            {
                                Nivel1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp1) >= 6 && Convert.ToDouble(PromTriEsp1) < 8)
                                {
                                    Nivel1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp1) >= 8 && Convert.ToDouble(PromTriEsp1) < 10)
                                    {
                                        Nivel1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriEsp1) == 10)
                                        {
                                            Nivel1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivel1 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Matematicas--------------------------------------------------------
                            double PromMat31 = 0;

                            PromMat31 = Convert.ToDouble(CalifSept[1]) + Convert.ToDouble(CalifOctu[1]) + Convert.ToDouble(CalifNovi[1]);
                            PromMat31 = PromMat31 / 3;
                            PromTriMat1 = PromMat31.ToString("0.#");//Promedio del primer trimestre de matematicas

                            //Asignacion del nivel del primer trimestre de matematicas
                            if (Convert.ToDouble(PromTriMat1) >= 5 && Convert.ToDouble(PromTriMat1) < 6)
                            {
                                NivelMat1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat1) >= 6 && Convert.ToDouble(PromTriMat1) < 8)
                                {
                                    NivelMat1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat1) >= 8 && Convert.ToDouble(PromTriMat1) < 10)
                                    {
                                        NivelMat1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriMat1) == 10)
                                        {
                                            NivelMat1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelMat1 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ingles-------------------------------------------------
                            double PromIng31 = 0;

                            PromIng31 = Convert.ToDouble(CalifSept[2]) + Convert.ToDouble(CalifOctu[2]) + Convert.ToDouble(CalifNovi[2]);
                            PromIng31 = PromIng31 / 3;
                            PromTriIng1 = PromIng31.ToString("0.#");//Promedio del primer trimestre de Ingles

                            //Asignacion del nivel del primer trimestre de Ingles
                            if (Convert.ToDouble(PromTriIng1) >= 5 && Convert.ToDouble(PromTriIng1) < 6)
                            {
                                NivelIng1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng1) >= 6 && Convert.ToDouble(PromTriIng1) < 8)
                                {
                                    NivelIng1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng1) >= 8 && Convert.ToDouble(PromTriIng1) < 10)
                                    {
                                        NivelIng1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriIng1) == 10)
                                        {
                                            NivelIng1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelIng1 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                            double Promcn31 = 0;

                            Promcn31 = Convert.ToDouble(CalifSept[3]) + Convert.ToDouble(CalifOctu[3]) + Convert.ToDouble(CalifNovi[3]);
                            Promcn31 = Promcn31 / 3;
                            PromTricn1 = Promcn31.ToString("0.#");//Promedio del primer trimestre de Ciencias

                            //Asignacion del nivel del primer trimestre de Ciencias
                            if (Convert.ToDouble(PromTricn1) >= 5 && Convert.ToDouble(PromTricn1) < 6)
                            {
                                Nivelcn1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn1) >= 6 && Convert.ToDouble(PromTricn1) < 8)
                                {
                                    Nivelcn1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn1) >= 8 && Convert.ToDouble(PromTricn1) < 10)
                                    {
                                        Nivelcn1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTricn1) == 10)
                                        {
                                            Nivelcn1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivelcn1 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Geografia-------------------------------------------------
                            double PromGeo31 = 0;

                            PromGeo31 = Convert.ToDouble(CalifSept[4]) + Convert.ToDouble(CalifOctu[4]) + Convert.ToDouble(CalifNovi[4]);
                            PromGeo31 = PromGeo31 / 3;
                            PromTriGeo1 = PromGeo31.ToString("0.#");//Promedio del primer trimestre de Geografia

                            //Asignacion del nivel del primer trimestre de Geografia
                            if (Convert.ToDouble(PromTriGeo1) >= 5 && Convert.ToDouble(PromTriGeo1) < 6)
                            {
                                NivelGeo1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo1) >= 6 && Convert.ToDouble(PromTriGeo1) < 8)
                                {
                                    NivelGeo1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo1) >= 8 && Convert.ToDouble(PromTriGeo1) < 10)
                                    {
                                        NivelGeo1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGeo1) == 10)
                                        {
                                            NivelGeo1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGeo1 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Historia--------------------------------------------------------
                            double PromHis31 = 0;

                            PromHis31 = Convert.ToDouble(CalifSept[5]) + Convert.ToDouble(CalifOctu[5]) + Convert.ToDouble(CalifNovi[5]);
                            PromHis31 = PromHis31 / 3;
                            PromTriHis1 = PromHis31.ToString("0.#");//Promedio del primer trimestre de Historia

                            //Asignacion del nivel del primer trimestre de Historia
                            if (Convert.ToDouble(PromTriHis1) >= 5 && Convert.ToDouble(PromTriHis1) < 6)
                            {
                                NivelHis1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis1) >= 6 && Convert.ToDouble(PromTriHis1) < 8)
                                {
                                    NivelHis1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis1) >= 8 && Convert.ToDouble(PromTriHis1) < 10)
                                    {
                                        NivelHis1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriHis1) == 10)
                                        {
                                            NivelHis1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelHis1 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Formacion Civica y etica-------------------------------------------------
                            double PromCiv31 = 0;

                            PromCiv31 = Convert.ToDouble(CalifSept[6]) + Convert.ToDouble(CalifOctu[6]) + Convert.ToDouble(CalifNovi[6]);
                            PromCiv31 = PromCiv31 / 3;
                            PromTriCiv1 = PromCiv31.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                            //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                            if (Convert.ToDouble(PromTriCiv1) >= 5 && Convert.ToDouble(PromTriCiv1) < 6)
                            {
                                NivelCiv1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv1) >= 6 && Convert.ToDouble(PromTriCiv1) < 8)
                                {
                                    NivelCiv1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv1) >= 8 && Convert.ToDouble(PromTriCiv1) < 10)
                                    {
                                        NivelCiv1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriCiv1) == 10)
                                        {
                                            NivelCiv1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelCiv1 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Artes--------------------------------------------------------
                            double PromArt31 = 0;

                            PromArt31 = Convert.ToDouble(CalifSept[7]) + Convert.ToDouble(CalifOctu[7]) + Convert.ToDouble(CalifNovi[7]);
                            PromArt31 = PromArt31 / 3;
                            PromTriArt1 = PromArt31.ToString("0.#");//Promedio del primer trimestre de Artes

                            //Asignacion del nivel del primer trimestre de Artes
                            if (Convert.ToDouble(PromTriArt1) >= 5 && Convert.ToDouble(PromTriArt1) < 6)
                            {
                                NivelArt1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt1) >= 6 && Convert.ToDouble(PromTriArt1) < 8)
                                {
                                    NivelArt1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt1) >= 8 && Convert.ToDouble(PromTriArt1) < 10)
                                    {
                                        NivelArt1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriArt1) == 10)
                                        {
                                            NivelArt1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelArt1 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ed. Fisica-------------------------------------------------
                            double PromFis31 = 0;

                            PromFis31 = Convert.ToDouble(CalifSept[8]) + Convert.ToDouble(CalifOctu[8]) + Convert.ToDouble(CalifNovi[8]);
                            PromFis31 = PromFis31 / 3;
                            PromTriFis1 = PromFis31.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                            //Asignacion del nivel del primer trimestre de Ed. Fisica
                            if (Convert.ToDouble(PromTriFis1) >= 5 && Convert.ToDouble(PromTriFis1) < 6)
                            {
                                NivelFis1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis1) >= 6 && Convert.ToDouble(PromTriFis1) < 8)
                                {
                                    NivelFis1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis1) >= 8 && Convert.ToDouble(PromTriFis1) < 10)
                                    {
                                        NivelFis1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriFis1) == 10)
                                        {
                                            NivelFis1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelFis1 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                            double PromSoc31 = 0;

                            PromSoc31 = Convert.ToDouble(CalifSept[9]) + Convert.ToDouble(CalifOctu[9]) + Convert.ToDouble(CalifNovi[9]);
                            PromSoc31 = PromSoc31 / 3;
                            PromTriSoc1 = PromSoc31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                            //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                            if (Convert.ToDouble(PromTriSoc1) >= 5 && Convert.ToDouble(PromTriSoc1) < 6)
                            {
                                NivelSoc1 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc1) >= 6 && Convert.ToDouble(PromTriSoc1) < 8)
                                {
                                    NivelSoc1 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc1) >= 8 && Convert.ToDouble(PromTriSoc1) < 10)
                                    {
                                        NivelSoc1 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSoc1) == 10)
                                        {
                                            NivelSoc1 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelSoc1 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Promedio de materias desarrollo social primer trimestre-------------------------------------------------
                            double PromDes31 = 0;

                            PromDes31 = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriSoc1);
                            PromDes31 = PromDes31 / 3;
                            PromTriDes1 = PromDes31.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriDes1) >= 5 && Convert.ToDouble(PromTriDes1) < 6)
                            {
                                NivelPromDes = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes1) >= 6 && Convert.ToDouble(PromTriDes1) < 8)
                                {
                                    NivelPromDes = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes1) >= 8 && Convert.ToDouble(PromTriDes1) < 10)
                                    {
                                        NivelPromDes = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriDes1) == 10)
                                        {
                                            NivelPromDes = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromDes = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Inasistencias----------------------------------------------------
                            double SumaInasis31 = 0;

                            SumaInasis31 = Convert.ToDouble(CalifSept[10]) + Convert.ToDouble(CalifOctu[10]) + Convert.ToDouble(CalifNovi[10]);
                            SumaInasisTri1 = SumaInasis31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                        }
                        else
                        {

                            for (int i = 0; i < 11; i++)
                            {
                                CalifNovi[i] = " ";
                            }
                        }
                        conn3.Close();

                        //Diciembre-----------------------------------------------------------------
                        string CalifDic = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diciembre'";

                        MySqlConnection conn4;
                        MySqlCommand com4;

                        conn4 = new MySqlConnection(conexion);
                        conn4.Open();

                        com4 = new MySqlCommand(CalifDic, conn4);

                        MySqlDataReader myreader4 = com4.ExecuteReader();

                        string[] CalifDici = new string[11];
                        string PromDic = " ";
                        string PromDiciGeneral = " ";

                        if (myreader4.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader4.Read())//Agrega calificaciones
                            {
                                double[] CalifDicitemp = new double[11];
                                CalifDicitemp[L] = Convert.ToDouble(myreader4["CalificacionMen"]);
                                CalifDici[L] = CalifDicitemp[L].ToString("0.#");
                                L++;
                            }

                            double PromDicTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromDicTemp = PromDicTemp + Convert.ToDouble(CalifDici[i]);
                            }

                            PromDicTemp = PromDicTemp / 9;
                            PromDic = PromDicTemp.ToString("0.#");//Promedio de diciembre

                            double PromDiciGen = 0;

                            PromDiciGen = Convert.ToDouble(PromDic) + Convert.ToDouble(CalifDici[9]);
                            PromDiciGen = PromDiciGen / 2;
                            PromDiciGeneral = PromDiciGen.ToString("0.#"); //Promedio general de diciembre
                        }
                        else
                        {

                            for (int i = 0; i < 11; i++)
                            {
                                CalifDici[i] = " ";
                            }
                        }
                        conn4.Close();
                        //Enero-----------------------------------------------------------------
                        string CalifEne = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Enero'";

                        MySqlConnection conn5;
                        MySqlCommand com5;

                        conn5 = new MySqlConnection(conexion);
                        conn5.Open();

                        com5 = new MySqlCommand(CalifEne, conn5);

                        MySqlDataReader myreader5 = com5.ExecuteReader();

                        string[] CalifEner = new string[11];
                        string PromEne = " ";
                        string PromEnerGeneral = " ";

                        if (myreader5.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader5.Read())//Agrega calificaciones
                            {
                                double[] CalifEnertemp = new double[11];
                                CalifEnertemp[L] = Convert.ToDouble(myreader5["CalificacionMen"]);
                                CalifEner[L] = CalifEnertemp[L].ToString("0.#");
                                L++;
                            }

                            double PromEneTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromEneTemp = PromEneTemp + Convert.ToDouble(CalifEner[i]);
                            }

                            PromEneTemp = PromEneTemp / 9;
                            PromEne = PromEneTemp.ToString("0.#");//Promedio de enero

                            double PromEnerGen = 0;

                            PromEnerGen = Convert.ToDouble(PromEne) + Convert.ToDouble(CalifEner[9]);
                            PromEnerGen = PromEnerGen / 2;
                            PromEnerGeneral = PromEnerGen.ToString("0.#"); //Promedio general de enero
                        }
                        else
                        {

                            for (int i = 0; i < 11; i++)
                            {
                                CalifEner[i] = " ";
                            }
                        }
                        conn5.Close();

                        //Febrero-----------------------------------------------------------------
                        string CalifFeb = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Febrero'";

                        MySqlConnection conn6;
                        MySqlCommand com6;

                        conn6 = new MySqlConnection(conexion);
                        conn6.Open();

                        com6 = new MySqlCommand(CalifFeb, conn6);

                        MySqlDataReader myreader6 = com6.ExecuteReader();

                        string[] CalifFebr = new string[11];
                        string PromFeb = " ";
                        string PromTriEsp2 = " ";
                        string PromTriMat2 = " ";
                        string PromTriIng2 = " ";
                        string PromTricn2 = " ";
                        string PromTriGeo2 = " ";
                        string PromTriHis2 = " ";
                        string PromTriCiv2 = " ";
                        string PromTriArt2 = " ";
                        string PromTriFis2 = " ";
                        string PromTriSoc2 = " ";
                        string PromTriSen2 = " ";
                        string SumaInasisTri2 = " ";
                        string Nivel2 = " ";
                        string NivelMat2 = " ";
                        string NivelIng2 = " ";
                        string Nivelcn2 = " ";
                        string NivelGeo2 = " ";
                        string NivelHis2 = " ";
                        string NivelCiv2 = " ";
                        string NivelArt2 = " ";
                        string NivelFis2 = " ";
                        string NivelSoc2 = " ";
                        string NivelPromSen2 = " ";
                        string PromFebrGeneral = " ";
                        string PromTriGen2 = " ";
                        string NivelGen2 = " ";

                        string NivelPromDes2 = " ";
                        string PromTriDes2 = " ";

                        if (myreader6.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader6.Read())//Agrega calificaciones
                            {
                                double[] CalifFebrtemp = new double[11];
                                CalifFebrtemp[L] = Convert.ToDouble(myreader6["CalificacionMen"]);
                                CalifFebr[L] = CalifFebrtemp[L].ToString("0.#");
                                L++;
                            }

                            double PromFebTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromFebTemp = PromFebTemp + Convert.ToDouble(CalifFebr[i]);
                            }

                            PromFebTemp = PromFebTemp / 9;
                            PromFeb = PromFebTemp.ToString("0.#");//Promedio de febrero

                            double PromFebrGen = 0;

                            PromFebrGen = Convert.ToDouble(PromFeb) + Convert.ToDouble(CalifFebr[9]);
                            PromFebrGen = PromFebrGen / 2;
                            PromFebrGeneral = PromFebrGen.ToString("0.#"); //Promedio general de febrero

                            //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                            double PromSen32 = 0;

                            PromSen32 = Convert.ToDouble(PromEne) + Convert.ToDouble(PromDic) + Convert.ToDouble(PromFeb);
                            PromSen32 = PromSen32 / 3;
                            PromTriSen2 = PromSen32.ToString("0.#");//Promedio del segundo trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriSen1) >= 5 && Convert.ToDouble(PromTriSen1) < 6)
                            {
                                NivelPromSen2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen1) >= 6 && Convert.ToDouble(PromTriSen1) < 8)
                                {
                                    NivelPromSen2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen1) >= 8 && Convert.ToDouble(PromTriSen1) < 10)
                                    {
                                        NivelPromSen2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSen1) == 10)
                                        {
                                            NivelPromSen2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromSen2 = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Promedio General segundo trimestre-------------------------------------------------
                            double PromGen32 = 0;

                            PromGen32 = Convert.ToDouble(PromDiciGeneral) + Convert.ToDouble(PromEnerGeneral) + Convert.ToDouble(PromFebrGeneral);
                            PromGen32 = PromGen32 / 3;
                            PromTriGen2 = PromGen32.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriGen2) >= 5 && Convert.ToDouble(PromTriGen2) < 6)
                            {
                                NivelGen2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen2) >= 6 && Convert.ToDouble(PromTriGen2) < 8)
                                {
                                    NivelGen2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen2) >= 8 && Convert.ToDouble(PromTriGen2) < 10)
                                    {
                                        NivelGen2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGen2) == 10)
                                        {
                                            NivelGen2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGen2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Español-------------------------------------------------
                            double PromEsp32 = 0;

                            PromEsp32 = Convert.ToDouble(CalifDici[0]) + Convert.ToDouble(CalifEner[0]) + Convert.ToDouble(CalifFebr[0]);
                            PromEsp32 = PromEsp32 / 3;
                            PromTriEsp2 = PromEsp32.ToString("0.#");//Promedio del primer trimestre de español

                            //Asignacion del nivel del primer trimestre de español
                            if (Convert.ToDouble(PromTriEsp2) >= 5 && Convert.ToDouble(PromTriEsp2) < 6)
                            {
                                Nivel2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp2) >= 6 && Convert.ToDouble(PromTriEsp2) < 8)
                                {
                                    Nivel2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp2) >= 8 && Convert.ToDouble(PromTriEsp2) < 10)
                                    {
                                        Nivel2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriEsp2) == 10)
                                        {
                                            Nivel2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivel2 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Matematicas--------------------------------------------------------
                            double PromMat32 = 0;

                            PromMat32 = Convert.ToDouble(CalifDici[1]) + Convert.ToDouble(CalifEner[1]) + Convert.ToDouble(CalifFebr[1]);
                            PromMat32 = PromMat32 / 3;
                            PromTriMat2 = PromMat32.ToString("0.#");//Promedio del primer trimestre de matematicas

                            //Asignacion del nivel del primer trimestre de matematicas
                            if (Convert.ToDouble(PromTriMat2) >= 5 && Convert.ToDouble(PromTriMat2) < 6)
                            {
                                NivelMat2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat2) >= 6 && Convert.ToDouble(PromTriMat2) < 8)
                                {
                                    NivelMat2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat2) >= 8 && Convert.ToDouble(PromTriMat2) < 10)
                                    {
                                        NivelMat2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriMat2) == 10)
                                        {
                                            NivelMat2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelMat2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ingles-------------------------------------------------
                            double PromIng32 = 0;

                            PromIng32 = Convert.ToDouble(CalifDici[2]) + Convert.ToDouble(CalifEner[2]) + Convert.ToDouble(CalifFebr[2]);
                            PromIng32 = PromIng32 / 3;
                            PromTriIng2 = PromIng32.ToString("0.#");//Promedio del primer trimestre de Ingles

                            //Asignacion del nivel del primer trimestre de Ingles
                            if (Convert.ToDouble(PromTriIng2) >= 5 && Convert.ToDouble(PromTriIng2) < 6)
                            {
                                NivelIng2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng2) >= 6 && Convert.ToDouble(PromTriIng2) < 8)
                                {
                                    NivelIng2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng2) >= 8 && Convert.ToDouble(PromTriIng2) < 10)
                                    {
                                        NivelIng2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriIng2) == 10)
                                        {
                                            NivelIng2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelIng2 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                            double Promcn32 = 0;

                            Promcn32 = Convert.ToDouble(CalifDici[3]) + Convert.ToDouble(CalifEner[3]) + Convert.ToDouble(CalifFebr[3]);
                            Promcn32 = Promcn32 / 3;
                            PromTricn2 = Promcn32.ToString("0.#");//Promedio del primer trimestre de Ciencias

                            //Asignacion del nivel del primer trimestre de Ciencias
                            if (Convert.ToDouble(PromTricn2) >= 5 && Convert.ToDouble(PromTricn2) < 6)
                            {
                                Nivelcn2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn2) >= 6 && Convert.ToDouble(PromTricn2) < 8)
                                {
                                    Nivelcn2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn2) >= 8 && Convert.ToDouble(PromTricn2) < 10)
                                    {
                                        Nivelcn2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTricn2) == 10)
                                        {
                                            Nivelcn2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivelcn2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Geografia-------------------------------------------------
                            double PromGeo32 = 0;

                            PromGeo32 = Convert.ToDouble(CalifDici[4]) + Convert.ToDouble(CalifEner[4]) + Convert.ToDouble(CalifFebr[4]);
                            PromGeo32 = PromGeo32 / 3;
                            PromTriGeo2 = PromGeo32.ToString("0.#");//Promedio del primer trimestre de Geografia

                            //Asignacion del nivel del primer trimestre de Geografia
                            if (Convert.ToDouble(PromTriGeo2) >= 5 && Convert.ToDouble(PromTriGeo2) < 6)
                            {
                                NivelGeo2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo2) >= 6 && Convert.ToDouble(PromTriGeo2) < 8)
                                {
                                    NivelGeo2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo2) >= 8 && Convert.ToDouble(PromTriGeo2) < 10)
                                    {
                                        NivelGeo2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGeo2) == 10)
                                        {
                                            NivelGeo2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGeo2 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Historia--------------------------------------------------------
                            double PromHis32 = 0;

                            PromHis32 = Convert.ToDouble(CalifDici[5]) + Convert.ToDouble(CalifEner[5]) + Convert.ToDouble(CalifFebr[5]);
                            PromHis32 = PromHis32 / 3;
                            PromTriHis2 = PromHis32.ToString("0.#");//Promedio del primer trimestre de Historia

                            //Asignacion del nivel del primer trimestre de Historia
                            if (Convert.ToDouble(PromTriHis2) >= 5 && Convert.ToDouble(PromTriHis2) < 6)
                            {
                                NivelHis2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis2) >= 6 && Convert.ToDouble(PromTriHis2) < 8)
                                {
                                    NivelHis2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis2) >= 8 && Convert.ToDouble(PromTriHis2) < 10)
                                    {
                                        NivelHis2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriHis2) == 10)
                                        {
                                            NivelHis2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelHis2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Formacion Civica y etica-------------------------------------------------
                            double PromCiv32 = 0;

                            PromCiv32 = Convert.ToDouble(CalifDici[6]) + Convert.ToDouble(CalifEner[6]) + Convert.ToDouble(CalifFebr[6]);
                            PromCiv32 = PromCiv32 / 3;
                            PromTriCiv2 = PromCiv32.ToString("0.#");//Promedio del primer trimestre de Formacion Civica y etica

                            //Asignacion del nivel del primer trimestre de Formacion Civica y etica
                            if (Convert.ToDouble(PromTriCiv2) >= 5 && Convert.ToDouble(PromTriCiv2) < 6)
                            {
                                NivelCiv2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv2) >= 6 && Convert.ToDouble(PromTriCiv2) < 8)
                                {
                                    NivelCiv2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv2) >= 8 && Convert.ToDouble(PromTriCiv2) < 10)
                                    {
                                        NivelCiv2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriCiv2) == 10)
                                        {
                                            NivelCiv2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelCiv2 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Artes--------------------------------------------------------
                            double PromArt32 = 0;

                            PromArt32 = Convert.ToDouble(CalifDici[7]) + Convert.ToDouble(CalifEner[7]) + Convert.ToDouble(CalifFebr[7]);
                            PromArt32 = PromArt32 / 3;
                            PromTriArt2 = PromArt32.ToString("0.#");//Promedio del primer trimestre de Artes

                            //Asignacion del nivel del primer trimestre de Artes
                            if (Convert.ToDouble(PromTriArt2) >= 5 && Convert.ToDouble(PromTriArt2) < 6)
                            {
                                NivelArt2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt2) >= 6 && Convert.ToDouble(PromTriArt2) < 8)
                                {
                                    NivelArt2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt2) >= 8 && Convert.ToDouble(PromTriArt2) < 10)
                                    {
                                        NivelArt2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriArt2) == 10)
                                        {
                                            NivelArt2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelArt2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ed. Fisica-------------------------------------------------
                            double PromFis32 = 0;

                            PromFis32 = Convert.ToDouble(CalifDici[8]) + Convert.ToDouble(CalifEner[8]) + Convert.ToDouble(CalifFebr[8]);
                            PromFis32 = PromFis32 / 3;
                            PromTriFis2 = PromFis32.ToString("0.#");//Promedio del primer trimestre de Ed. Fisica

                            //Asignacion del nivel del primer trimestre de Ed. Fisica
                            if (Convert.ToDouble(PromTriFis2) >= 5 && Convert.ToDouble(PromTriFis2) < 6)
                            {
                                NivelFis2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis2) >= 6 && Convert.ToDouble(PromTriFis2) < 8)
                                {
                                    NivelFis2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis2) >= 8 && Convert.ToDouble(PromTriFis2) < 10)
                                    {
                                        NivelFis2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriFis2) == 10)
                                        {
                                            NivelFis2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelFis2 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                            double PromSoc32 = 0;

                            PromSoc32 = Convert.ToDouble(CalifDici[9]) + Convert.ToDouble(CalifEner[9]) + Convert.ToDouble(CalifFebr[9]);
                            PromSoc32 = PromSoc32 / 3;
                            PromTriSoc2 = PromSoc32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                            //Asignacion del nivel del primer trimestre de Ed. Socioemocional
                            if (Convert.ToDouble(PromTriSoc2) >= 5 && Convert.ToDouble(PromTriSoc2) < 6)
                            {
                                NivelSoc2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc2) >= 6 && Convert.ToDouble(PromTriSoc2) < 8)
                                {
                                    NivelSoc2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc2) >= 8 && Convert.ToDouble(PromTriSoc2) < 10)
                                    {
                                        NivelSoc2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSoc2) == 10)
                                        {
                                            NivelSoc2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelSoc2 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Promedio de materias desarrollo social segundo trimestre-------------------------------------------------
                            double PromDes32 = 0;

                            PromDes32 = Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriSoc2);
                            PromDes32 = PromDes32 / 3;
                            PromTriDes2 = PromDes32.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriDes2) >= 5 && Convert.ToDouble(PromTriDes2) < 6)
                            {
                                NivelPromDes2 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes2) >= 6 && Convert.ToDouble(PromTriDes2) < 8)
                                {
                                    NivelPromDes2 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes2) >= 8 && Convert.ToDouble(PromTriDes2) < 10)
                                    {
                                        NivelPromDes2 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriDes2) == 10)
                                        {
                                            NivelPromDes2 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromDes2 = " ";
                                        }
                                    }
                                }
                            }

                            //-----------------------------Inasistencia-----------------------------------------------------
                            double SumaInasis32 = 0;

                            SumaInasis32 = Convert.ToDouble(CalifDici[10]) + Convert.ToDouble(CalifEner[10]) + Convert.ToDouble(CalifFebr[10]);
                            SumaInasisTri2 = SumaInasis32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifFebr[i] = " ";
                            }
                        }
                        conn6.Close();
                        //Marzo-----------------------------------------------------------------
                        string CalifMar = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Marzo'";

                        MySqlConnection conn7;
                        MySqlCommand com7;

                        conn7 = new MySqlConnection(conexion);
                        conn7.Open();

                        com7 = new MySqlCommand(CalifMar, conn7);

                        MySqlDataReader myreader7 = com7.ExecuteReader();

                        string[] CalifMarz = new string[11];
                        string PromMar = " ";
                        string PromMarzGeneral = " ";

                        if (myreader7.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader7.Read())//Agrega calificaciones
                            {
                                double[] CalifMarztemp = new double[11];
                                CalifMarztemp[L] = Convert.ToDouble(myreader7["CalificacionMen"]);
                                CalifMarz[L] = CalifMarztemp[L].ToString("0.#");
                                L++;
                            }

                            double PromMarTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromMarTemp = PromMarTemp + Convert.ToDouble(CalifMarz[i]);
                            }

                            PromMarTemp = PromMarTemp / 9;
                            PromMar = PromMarTemp.ToString("0.#");//Promedio de  marzo

                            double PromMarzGen = 0;

                            PromMarzGen = Convert.ToDouble(PromMar) + Convert.ToDouble(CalifMarz[9]);
                            PromMarzGen = PromMarzGen / 2;
                            PromMarzGeneral = PromMarzGen.ToString("0.#"); //Promedio general de marzo

                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifMarz[i] = " ";
                            }
                        }
                        conn7.Close();
                        //Abril-----------------------------------------------------------------
                        string CalifAbr = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Abril'";

                        MySqlConnection conn8;
                        MySqlCommand com8;

                        conn8 = new MySqlConnection(conexion);
                        conn8.Open();

                        com8 = new MySqlCommand(CalifAbr, conn8);

                        MySqlDataReader myreader8 = com8.ExecuteReader();

                        string[] CalifAbri = new string[11];
                        string PromAbr = " ";
                        string PromAbriGeneral = " ";

                        if (myreader8.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader8.Read())//Agrega calificaciones
                            {
                                double[] CalifAbritemp = new double[11];
                                CalifAbritemp[L] = Convert.ToDouble(myreader8["CalificacionMen"]);
                                CalifAbri[L] = CalifAbritemp[L].ToString("0.#");
                                L++;
                            }

                            double PromAbrTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromAbrTemp = PromAbrTemp + Convert.ToDouble(CalifAbri[i]);
                            }

                            PromAbrTemp = PromAbrTemp / 9;
                            PromAbr = PromAbrTemp.ToString("0.#");//Promedio de abril

                            double PromAbriGen = 0;

                            PromAbriGen = Convert.ToDouble(PromAbr) + Convert.ToDouble(CalifAbri[9]);
                            PromAbriGen = PromAbriGen / 2;
                            PromAbriGeneral = PromAbriGen.ToString("0.#"); //Promedio general de abril

                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifAbri[i] = " ";
                            }
                        }
                        conn8.Close();
                        //Mayo-----------------------------------------------------------------
                        string CalifMay = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Mayo'";

                        MySqlConnection conn9;
                        MySqlCommand com9;

                        conn9 = new MySqlConnection(conexion);
                        conn9.Open();

                        com9 = new MySqlCommand(CalifMay, conn9);

                        MySqlDataReader myreader9 = com9.ExecuteReader();

                        string[] CalifMayo = new string[11];
                        string PromMay = " ";
                        string PromMayoGeneral = " ";

                        if (myreader9.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader9.Read())//Agrega calificaciones
                            {
                                double[] CalifMayotemp = new double[11];
                                CalifMayotemp[L] = Convert.ToDouble(myreader9["CalificacionMen"]);
                                CalifMayo[L] = CalifMayotemp[L].ToString("0.#");
                                L++;
                            }

                            double PromMayTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromMayTemp = PromMayTemp + Convert.ToDouble(CalifMayo[i]);
                            }

                            PromMayTemp = PromMayTemp / 9;
                            PromMay = PromMayTemp.ToString("0.#");//Promedio de mayo

                            double PromMayoGen = 0;

                            PromMayoGen = Convert.ToDouble(PromMay) + Convert.ToDouble(CalifMayo[9]);
                            PromMayoGen = PromMayoGen / 2;
                            PromMayoGeneral = PromMayoGen.ToString("0.#"); //Promedio general de mayo
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifMayo[i] = " ";
                            }
                        }
                        conn9.Close();
                        //Junio-----------------------------------------------------------------
                        string CalifJun = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Junio'";

                        MySqlConnection conn10;
                        MySqlCommand com10;

                        conn10 = new MySqlConnection(conexion);
                        conn10.Open();

                        com10 = new MySqlCommand(CalifJun, conn10);

                        MySqlDataReader myreader10 = com10.ExecuteReader();

                        string[] CalifJuni = new string[11];
                        string PromJun = " ";
                        string PromTriEsp3 = " ";
                        string PromTriMat3 = " ";
                        string PromTriIng3 = " ";
                        string PromTricn3 = " ";
                        string PromTriGeo3 = " ";
                        string PromTriHis3 = " ";
                        string PromTriCiv3 = " ";
                        string PromTriArt3 = " ";
                        string PromTriFis3 = " ";
                        string PromTriSoc3 = " ";
                        string SumaInasisTri3 = " ";
                        string Nivel3 = " ";
                        string NivelMat3 = " ";
                        string NivelIng3 = " ";
                        string Nivelcn3 = " ";
                        string NivelGeo3 = " ";
                        string NivelHis3 = " ";
                        string NivelCiv3 = " ";
                        string NivelArt3 = " ";
                        string NivelFis3 = " ";
                        string NivelSoc3 = " ";
                        string PromTriGen3 = " ";
                        string NivelGen3 = " ";
                        string PromFinTriEsp = " ";
                        string NivelFin = " ";
                        string PromFinTriMat = " ";
                        string NivelMatFin = " ";
                        string PromFinTriIng = " ";
                        string NivelIngFin = " ";
                        string PromFinTricn = " ";
                        string NivelcnFin = " ";
                        string PromFinTriGeo = " ";
                        string NivelGeoFin = " ";
                        string PromFinTriHis = " ";
                        string NivelHisFin = " ";
                        string PromFinTriCiv = " ";
                        string NivelCivFin = " ";
                        string PromFinTriArt = " ";
                        string NivelArtFin = " ";
                        string PromFinTriFis = " ";
                        string NivelFisFin = " ";
                        string PromFinTriSoc = " ";
                        string NivelSocFin = " ";
                        string PromTriSen3 = " ";
                        string NivelPromSen3 = " ";
                        string PromTriSenFin = " ";
                        string NivelPromSenFin = " ";
                        string PromTriGenFin = " ";
                        string NivelGenFin = " ";
                        string SumInasisTriFin = " ";

                        string PromTriDes3 = " ";
                        string NivelPromDes3 = " ";
                        string PromTriDesFin = " ";
                        string NivelPromDesFin = " ";
                        string PorcAsistencias = " ";

                        string Si = " ";
                        string No = " ";
                        string Yes = " ";
                        string Nou = " ";
                        string Yes1 = " ";
                        string Nou1 = " ";
                        string Yes2 = " ";
                        string Nou2 = " ";
                        string Yes3 = " ";
                        string Nou3 = " ";
                        string Yes4 = " ";
                        string Nou4 = " ";
                        string PromMateriasIC = " ";
                        string SumAsisTriFin = " ";

                        string PromJuniGeneral = " ";

                        if (myreader10.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader10.Read())//Agrega calificaciones
                            {
                                double[] CalifJunitemp = new double[11];
                                CalifJunitemp[L] = Convert.ToDouble(myreader10["CalificacionMen"]);
                                CalifJuni[L] = CalifJunitemp[L].ToString("0.#");
                                L++;
                            }

                            double PromJunTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromJunTemp = PromJunTemp + Convert.ToDouble(CalifJuni[i]);
                            }

                            PromJunTemp = PromJunTemp / 9;
                            PromJun = PromJunTemp.ToString("0.#");//Promedio de junio

                            double PromJuniGen = 0;

                            PromJuniGen = Convert.ToDouble(PromJun) + Convert.ToDouble(CalifJuni[9]);
                            PromJuniGen = PromJuniGen / 2;
                            PromJuniGeneral = PromJuniGen.ToString("0.#"); //Promedio general de junio

                            //------------------------------------Promedio de materias sencillas segundo trimestre-------------------------------------------------
                            double PromSen33 = 0;

                            PromSen33 = Convert.ToDouble(PromMar) + Convert.ToDouble(PromAbr) + Convert.ToDouble(PromMay) + Convert.ToDouble(PromJun);
                            PromSen33 = PromSen33 / 4;
                            PromTriSen3 = PromSen33.ToString("0.#");//Promedio del segundo trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriSen3) >= 5 && Convert.ToDouble(PromTriSen3) < 6)
                            {
                                NivelPromSen3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSen3) >= 6 && Convert.ToDouble(PromTriSen3) < 8)
                                {
                                    NivelPromSen3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSen3) >= 8 && Convert.ToDouble(PromTriSen3) < 10)
                                    {
                                        NivelPromSen3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSen3) == 10)
                                        {
                                            NivelPromSen3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromSen3 = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                            double PromGen33 = 0;

                            PromGen33 = Convert.ToDouble(PromMarzGeneral) + Convert.ToDouble(PromAbriGeneral) + Convert.ToDouble(PromMayoGeneral) + Convert.ToDouble(PromJuniGeneral);
                            PromGen33 = PromGen33 / 4;
                            PromTriGen3 = PromGen33.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriGen3) >= 5 && Convert.ToDouble(PromTriGen3) < 6)
                            {
                                NivelGen3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen3) >= 6 && Convert.ToDouble(PromTriGen3) < 8)
                                {
                                    NivelGen3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen3) >= 8 && Convert.ToDouble(PromTriGen3) < 10)
                                    {
                                        NivelGen3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGen3) == 10)
                                        {
                                            NivelGen3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGen3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Español-------------------------------------------------

                            double PromEsp33 = 0;

                            PromEsp33 = Convert.ToDouble(CalifMarz[0]) + Convert.ToDouble(CalifAbri[0]) + Convert.ToDouble(CalifMayo[0]) + Convert.ToDouble(CalifJuni[0]);
                            PromEsp33 = PromEsp33 / 4;
                            PromTriEsp3 = PromEsp33.ToString("0.#");//Promedio del tercer trimestre de español

                            //Asignacion del nivel del tercer trimestre de español
                            if (Convert.ToDouble(PromTriEsp3) >= 5 && Convert.ToDouble(PromTriEsp3) < 6)
                            {
                                Nivel3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriEsp3) >= 6 && Convert.ToDouble(PromTriEsp3) < 8)
                                {
                                    Nivel3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriEsp3) >= 8 && Convert.ToDouble(PromTriEsp3) < 10)
                                    {
                                        Nivel3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriEsp3) == 10)
                                        {
                                            Nivel3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivel3 = " ";
                                        }
                                    }
                                }
                            }

                            //-------------------------------------Matematicas--------------------------------------------------------
                            double PromMat33 = 0;

                            PromMat33 = Convert.ToDouble(CalifMarz[1]) + Convert.ToDouble(CalifAbri[1]) + Convert.ToDouble(CalifMayo[1]) + Convert.ToDouble(CalifJuni[1]);
                            PromMat33 = PromMat33 / 4;
                            PromTriMat3 = PromMat33.ToString("0.#");//Promedio del primer trimestre de matematicas

                            //Asignacion del nivel del primer trimestre de matematicas
                            if (Convert.ToDouble(PromTriMat3) >= 5 && Convert.ToDouble(PromTriMat3) < 6)
                            {
                                NivelMat3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriMat3) >= 6 && Convert.ToDouble(PromTriMat3) < 8)
                                {
                                    NivelMat3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriMat3) >= 8 && Convert.ToDouble(PromTriMat3) < 10)
                                    {
                                        NivelMat3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriMat3) == 10)
                                        {
                                            NivelMat3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelMat3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ingles-------------------------------------------------
                            double PromIng33 = 0;

                            PromIng33 = Convert.ToDouble(CalifMarz[2]) + Convert.ToDouble(CalifAbri[2]) + Convert.ToDouble(CalifMayo[2]) + Convert.ToDouble(CalifJuni[2]);
                            PromIng33 = PromIng33 / 4;
                            PromTriIng3 = PromIng33.ToString("0.#");//Promedio del tercer trimestre de Ingles

                            //Asignacion del nivel del tercer trimestre de Ingles
                            if (Convert.ToDouble(PromTriIng3) >= 5 && Convert.ToDouble(PromTriIng3) < 6)
                            {
                                NivelIng3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriIng3) >= 6 && Convert.ToDouble(PromTriIng3) < 8)
                                {
                                    NivelIng3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriIng3) >= 8 && Convert.ToDouble(PromTriIng3) < 10)
                                    {
                                        NivelIng3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriIng3) == 10)
                                        {
                                            NivelIng3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelIng3 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ciencias Nacturales--------------------------------------------------------
                            double Promcn33 = 0;

                            Promcn33 = Convert.ToDouble(CalifMarz[3]) + Convert.ToDouble(CalifAbri[3]) + Convert.ToDouble(CalifMayo[3]) + Convert.ToDouble(CalifJuni[3]);
                            Promcn33 = Promcn33 / 4;
                            PromTricn3 = Promcn33.ToString("0.#");//Promedio del tercer trimestre de Ciencias

                            //Asignacion del nivel del tercer trimestre de Ciencias
                            if (Convert.ToDouble(PromTricn3) >= 5 && Convert.ToDouble(PromTricn3) < 6)
                            {
                                Nivelcn3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTricn3) >= 6 && Convert.ToDouble(PromTricn3) < 8)
                                {
                                    Nivelcn3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTricn3) >= 8 && Convert.ToDouble(PromTricn3) < 10)
                                    {
                                        Nivelcn3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTricn3) == 10)
                                        {
                                            Nivelcn3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            Nivelcn3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Geografia-------------------------------------------------
                            double PromGeo33 = 0;

                            PromGeo33 = Convert.ToDouble(CalifMarz[4]) + Convert.ToDouble(CalifAbri[4]) + Convert.ToDouble(CalifMayo[4]) + Convert.ToDouble(CalifJuni[4]);
                            PromGeo33 = PromGeo33 / 4;
                            PromTriGeo3 = PromGeo33.ToString("0.#");//Promedio del tercer trimestre de Geografia

                            //Asignacion del nivel del tercer trimestre de Geografia
                            if (Convert.ToDouble(PromTriGeo3) >= 5 && Convert.ToDouble(PromTriGeo3) < 6)
                            {
                                NivelGeo3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGeo3) >= 6 && Convert.ToDouble(PromTriGeo3) < 8)
                                {
                                    NivelGeo3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGeo3) >= 8 && Convert.ToDouble(PromTriGeo3) < 10)
                                    {
                                        NivelGeo3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGeo3) == 10)
                                        {
                                            NivelGeo3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGeo3 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Historia--------------------------------------------------------
                            double PromHis33 = 0;

                            PromHis33 = Convert.ToDouble(CalifMarz[5]) + Convert.ToDouble(CalifAbri[5]) + Convert.ToDouble(CalifMayo[5]) + Convert.ToDouble(CalifJuni[5]);
                            PromHis33 = PromHis33 / 4;
                            PromTriHis3 = PromHis33.ToString("0.#");//Promedio del tercer trimestre de Historia

                            //Asignacion del nivel del tercer trimestre de Historia
                            if (Convert.ToDouble(PromTriHis3) >= 5 && Convert.ToDouble(PromTriHis3) < 6)
                            {
                                NivelHis3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriHis3) >= 6 && Convert.ToDouble(PromTriHis3) < 8)
                                {
                                    NivelHis3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriHis3) >= 8 && Convert.ToDouble(PromTriHis3) < 10)
                                    {
                                        NivelHis3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriHis3) == 10)
                                        {
                                            NivelHis3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelHis3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Formacion Civica y etica-------------------------------------------------
                            double PromCiv33 = 0;

                            PromCiv33 = Convert.ToDouble(CalifMarz[6]) + Convert.ToDouble(CalifAbri[6]) + Convert.ToDouble(CalifMayo[6]) + Convert.ToDouble(CalifJuni[6]);
                            PromCiv33 = PromCiv33 / 4;
                            PromTriCiv3 = PromCiv33.ToString("0.#");//Promedio del tercer trimestre de Formacion Civica y etica

                            //Asignacion del nivel del tercer trimestre de Formacion Civica y etica
                            if (Convert.ToDouble(PromTriCiv3) >= 5 && Convert.ToDouble(PromTriCiv3) < 6)
                            {
                                NivelCiv3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriCiv3) >= 6 && Convert.ToDouble(PromTriCiv3) < 8)
                                {
                                    NivelCiv3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriCiv3) >= 8 && Convert.ToDouble(PromTriCiv3) < 10)
                                    {
                                        NivelCiv3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriCiv3) == 10)
                                        {
                                            NivelCiv3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelCiv3 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Artes--------------------------------------------------------
                            double PromArt33 = 0;

                            PromArt33 = Convert.ToDouble(CalifMarz[7]) + Convert.ToDouble(CalifAbri[7]) + Convert.ToDouble(CalifMayo[7]) + Convert.ToDouble(CalifJuni[7]);
                            PromArt33 = PromArt33 / 4;
                            PromTriArt3 = PromArt33.ToString("0.#");//Promedio del tercer trimestre de Artes

                            //Asignacion del nivel del tercer trimestre de Artes
                            if (Convert.ToDouble(PromTriArt3) >= 5 && Convert.ToDouble(PromTriArt3) < 6)
                            {
                                NivelArt3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriArt3) >= 6 && Convert.ToDouble(PromTriArt3) < 8)
                                {
                                    NivelArt3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriArt3) >= 8 && Convert.ToDouble(PromTriArt3) < 10)
                                    {
                                        NivelArt3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriArt3) == 10)
                                        {
                                            NivelArt3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelArt3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Ed. Fisica-------------------------------------------------
                            double PromFis33 = 0;

                            PromFis33 = Convert.ToDouble(CalifMarz[8]) + Convert.ToDouble(CalifAbri[8]) + Convert.ToDouble(CalifMayo[8]) + Convert.ToDouble(CalifJuni[8]);
                            PromFis33 = PromFis33 / 4;
                            PromTriFis3 = PromFis33.ToString("0.#");//Promedio del tercer trimestre de Ed. Fisica

                            //Asignacion del nivel del tercer trimestre de Ed. Fisica
                            if (Convert.ToDouble(PromTriFis3) >= 5 && Convert.ToDouble(PromTriFis3) < 6)
                            {
                                NivelFis3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriFis3) >= 6 && Convert.ToDouble(PromTriFis3) < 8)
                                {
                                    NivelFis3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriFis3) >= 8 && Convert.ToDouble(PromTriFis3) < 10)
                                    {
                                        NivelFis3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriFis3) == 10)
                                        {
                                            NivelFis3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelFis3 = " ";
                                        }
                                    }
                                }
                            }
                            //-------------------------------------Ed. Socioemocional--------------------------------------------------------
                            double PromSoc33 = 0;

                            PromSoc33 = Convert.ToDouble(CalifMarz[9]) + Convert.ToDouble(CalifAbri[9]) + Convert.ToDouble(CalifMayo[9]) + Convert.ToDouble(CalifJuni[9]);
                            PromSoc33 = PromSoc33 / 4;
                            PromTriSoc3 = PromSoc33.ToString("0.#");//Promedio del tercer trimestre de Ed. Socioemocional

                            //Asignacion del nivel del tercer trimestre de Ed. Socioemocional
                            if (Convert.ToDouble(PromTriSoc3) >= 5 && Convert.ToDouble(PromTriSoc3) < 6)
                            {
                                NivelSoc3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSoc3) >= 6 && Convert.ToDouble(PromTriSoc3) < 8)
                                {
                                    NivelSoc3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSoc3) >= 8 && Convert.ToDouble(PromTriSoc3) < 10)
                                    {
                                        NivelSoc3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSoc3) == 10)
                                        {
                                            NivelSoc3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelSoc3 = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                            double PromDes33 = 0;

                            PromDes33 = Convert.ToDouble(PromTriArt3) + Convert.ToDouble(PromTriFis3) + Convert.ToDouble(PromTriSoc3);
                            PromDes33 = PromDes33 / 3;
                            PromTriDes3 = PromDes33.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriDes3) >= 5 && Convert.ToDouble(PromTriDes3) < 6)
                            {
                                NivelPromDes3 = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDes3) >= 6 && Convert.ToDouble(PromTriDes3) < 8)
                                {
                                    NivelPromDes3 = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDes3) >= 8 && Convert.ToDouble(PromTriDes3) < 10)
                                    {
                                        NivelPromDes3 = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriDes3) == 10)
                                        {
                                            NivelPromDes3 = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromDes3 = " ";
                                        }
                                    }
                                }
                            }

                            //-----------------------------Inasistencia-----------------------------------------------------
                            double SumaInasis33 = 0;

                            SumaInasis33 = Convert.ToDouble(CalifMarz[10]) + Convert.ToDouble(CalifAbri[10]) + Convert.ToDouble(CalifMayo[10]) + Convert.ToDouble(CalifJuni[10]);
                            SumaInasisTri3 = SumaInasis33.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                            //------------------------------------Promedio de materias sencillas final trimestres-------------------------------------------------
                            double PromSenFin = 0;

                            PromSenFin = Convert.ToDouble(PromTriSen1) + Convert.ToDouble(PromTriSen2) + Convert.ToDouble(PromTriSen3);
                            PromSenFin = PromSenFin / 3;
                            PromTriSenFin = PromSenFin.ToString("0.#");//Promedio del segundo trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriSenFin) >= 5 && Convert.ToDouble(PromTriSenFin) < 6)
                            {
                                NivelPromSenFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriSenFin) >= 6 && Convert.ToDouble(PromTriSenFin) < 8)
                                {
                                    NivelPromSenFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriSenFin) >= 8 && Convert.ToDouble(PromTriSenFin) < 10)
                                    {
                                        NivelPromSenFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriSenFin) == 10)
                                        {
                                            NivelPromSenFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromSenFin = " ";
                                        }
                                    }
                                }
                            }

                            //------------------------------------Promedio General tercer trimestre-------------------------------------------------
                            double PromGen3Fin = 0;

                            PromGen3Fin = Convert.ToDouble(PromTriGen1) + Convert.ToDouble(PromTriGen2) + Convert.ToDouble(PromTriGen3);
                            PromGen3Fin = PromGen3Fin / 3;
                            PromTriGenFin = PromGen3Fin.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriGenFin) >= 5 && Convert.ToDouble(PromTriGenFin) < 6)
                            {
                                NivelGenFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGenFin) >= 6 && Convert.ToDouble(PromTriGenFin) < 8)
                                {
                                    NivelGenFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGenFin) >= 8 && Convert.ToDouble(PromTriGenFin) < 10)
                                    {
                                        NivelGenFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGenFin) == 10)
                                        {
                                            NivelGenFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGenFin = " ";
                                        }
                                    }
                                }
                            }

                            if (PromGen3Fin >= 6)
                            {
                                Si = "X";
                                No = " ";
                            }
                            else
                            {
                                Si = " ";
                                No = "X";
                            }
                            //--------------------------------------------Nivel general mayor a II ---------------------------------
                            if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                            {
                                Yes4 = "Yes";
                                Nou4 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                                {
                                    Yes4 = "Yes";
                                    Nou4 = "Off";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriGen1) > 5.9 && Convert.ToDouble(PromTriGen3) > 5.9)
                                    {
                                        Yes4 = "Yes";
                                        Nou4 = "Off";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriGen3) > 5.9 && Convert.ToDouble(PromTriGen2) > 5.9)
                                        {
                                            Yes4 = "Yes";
                                            Nou4 = "Off";
                                        }
                                        else
                                        {
                                            Yes4 = "Off";
                                            Nou4 = "Yes";
                                        }
                                    }
                                }
                            }

                            //--------------------------------------Promedio final de español trimestral--------------------------------
                            double PromFin = 0;

                            PromFin = Convert.ToDouble(PromTriEsp1) + Convert.ToDouble(PromTriEsp2) + Convert.ToDouble(PromTriEsp3);
                            PromFin = PromFin / 3;
                            PromFinTriEsp = PromFin.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriEsp) >= 5 && Convert.ToDouble(PromFinTriEsp) < 6)
                            {
                                NivelFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriEsp) >= 6 && Convert.ToDouble(PromFinTriEsp) < 8)
                                {
                                    NivelFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriEsp) >= 8 && Convert.ToDouble(PromFinTriEsp) < 10)
                                    {
                                        NivelFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriEsp) == 10)
                                        {
                                            NivelFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelFin = " ";
                                        }
                                    }
                                }
                            }

                            if (Convert.ToDouble(PromFinTriEsp) > 5.9 && Convert.ToDouble(PromFinTriEsp) <= 10)
                            {
                                Yes = "Yes";
                                Nou = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriEsp) <= 5.9)
                                {
                                    Yes = "Off";
                                    Nou = "Yes";
                                }
                            }
                            //--------------------------------------Promedio final de matematicas trimestral--------------------------------
                            double PromFinMat = 0;

                            PromFinMat = Convert.ToDouble(PromTriMat1) + Convert.ToDouble(PromTriMat2) + Convert.ToDouble(PromTriMat3);
                            PromFinMat = PromFinMat / 3;
                            PromFinTriMat = PromFinMat.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriMat) >= 5 && Convert.ToDouble(PromFinTriMat) < 6)
                            {
                                NivelMatFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriMat) >= 6 && Convert.ToDouble(PromFinTriMat) < 8)
                                {
                                    NivelMatFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriMat) >= 8 && Convert.ToDouble(PromFinTriMat) < 10)
                                    {
                                        NivelMatFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriMat) == 10)
                                        {
                                            NivelMatFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelMatFin = " ";
                                        }
                                    }
                                }
                            }

                            if (Convert.ToDouble(PromFinTriMat) > 5.9 && Convert.ToDouble(PromFinTriMat) <= 10)
                            {
                                Yes1 = "Yes";
                                Nou1 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriMat) <= 5.9)
                                {
                                    Yes1 = "Off";
                                    Nou1 = "Yes";
                                }
                            }
                            //--------------------------------------Promedio final de Ingles trimestral--------------------------------
                            double PromFinIng = 0;

                            PromFinIng = Convert.ToDouble(PromTriIng1) + Convert.ToDouble(PromTriIng2) + Convert.ToDouble(PromTriIng3);
                            PromFinIng = PromFinIng / 3;
                            PromFinTriIng = PromFinIng.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriIng) >= 5 && Convert.ToDouble(PromFinTriIng) < 6)
                            {
                                NivelIngFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriIng) >= 6 && Convert.ToDouble(PromFinTriIng) < 8)
                                {
                                    NivelIngFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriIng) >= 8 && Convert.ToDouble(PromFinTriIng) < 10)
                                    {
                                        NivelIngFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriIng) == 10)
                                        {
                                            NivelIngFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelIngFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Ciencias Naturales trimestral--------------------------------
                            double PromFincn = 0;

                            PromFincn = Convert.ToDouble(PromTricn1) + Convert.ToDouble(PromTricn2) + Convert.ToDouble(PromTricn3);
                            PromFincn = PromFincn / 3;
                            PromFinTricn = PromFincn.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTricn) >= 5 && Convert.ToDouble(PromFinTricn) < 6)
                            {
                                NivelcnFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTricn) >= 6 && Convert.ToDouble(PromFinTricn) < 8)
                                {
                                    NivelcnFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTricn) >= 8 && Convert.ToDouble(PromFinTricn) < 10)
                                    {
                                        NivelcnFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTricn) == 10)
                                        {
                                            NivelcnFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelcnFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Geografia trimestral--------------------------------
                            double PromFinGeo = 0;

                            PromFinGeo = Convert.ToDouble(PromTriGeo1) + Convert.ToDouble(PromTriGeo2) + Convert.ToDouble(PromTriGeo3);
                            PromFinGeo = PromFinGeo / 3;
                            PromFinTriGeo = PromFinGeo.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriGeo) >= 5 && Convert.ToDouble(PromFinTriGeo) < 6)
                            {
                                NivelGeoFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriGeo) >= 6 && Convert.ToDouble(PromFinTriGeo) < 8)
                                {
                                    NivelGeoFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriGeo) >= 8 && Convert.ToDouble(PromFinTriGeo) < 10)
                                    {
                                        NivelGeoFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriGeo) == 10)
                                        {
                                            NivelGeoFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelGeoFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Historia trimestral--------------------------------
                            double PromFinHis = 0;

                            PromFinHis = Convert.ToDouble(PromTriHis1) + Convert.ToDouble(PromTriHis2) + Convert.ToDouble(PromTriHis3);
                            PromFinHis = PromFinHis / 3;
                            PromFinTriHis = PromFinHis.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriHis) >= 5 && Convert.ToDouble(PromFinTriHis) < 6)
                            {
                                NivelHisFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriHis) >= 6 && Convert.ToDouble(PromFinTriHis) < 8)
                                {
                                    NivelHisFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriHis) >= 8 && Convert.ToDouble(PromFinTriHis) < 10)
                                    {
                                        NivelHisFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriHis) == 10)
                                        {
                                            NivelHisFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelHisFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Formacion Civica y etica trimestral--------------------------------
                            double PromFinCiv = 0;

                            PromFinCiv = Convert.ToDouble(PromTriCiv1) + Convert.ToDouble(PromTriCiv2) + Convert.ToDouble(PromTriCiv3);
                            PromFinCiv = PromFinCiv / 3;
                            PromFinTriCiv = PromFinCiv.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriCiv) >= 5 && Convert.ToDouble(PromFinTriCiv) < 6)
                            {
                                NivelCivFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriCiv) >= 6 && Convert.ToDouble(PromFinTriCiv) < 8)
                                {
                                    NivelCivFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriCiv) >= 8 && Convert.ToDouble(PromFinTriCiv) < 10)
                                    {
                                        NivelCivFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriCiv) == 10)
                                        {
                                            NivelCivFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelCivFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Artes trimestral--------------------------------
                            double PromFinArt = 0;

                            PromFinArt = Convert.ToDouble(PromTriArt1) + Convert.ToDouble(PromTriArt2) + Convert.ToDouble(PromTriArt3);
                            PromFinArt = PromFinArt / 3;
                            PromFinTriArt = PromFinArt.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriArt) >= 5 && Convert.ToDouble(PromFinTriArt) < 6)
                            {
                                NivelArtFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriArt) >= 6 && Convert.ToDouble(PromFinTriArt) < 8)
                                {
                                    NivelArtFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriArt) >= 8 && Convert.ToDouble(PromFinTriArt) < 10)
                                    {
                                        NivelArtFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriArt) == 10)
                                        {
                                            NivelArtFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelArtFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Ed. Fisica trimestral--------------------------------
                            double PromFinFis = 0;

                            PromFinFis = Convert.ToDouble(PromTriFis1) + Convert.ToDouble(PromTriFis2) + Convert.ToDouble(PromTriFis3);
                            PromFinFis = PromFinFis / 3;
                            PromFinTriFis = PromFinFis.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriFis) >= 5 && Convert.ToDouble(PromFinTriFis) < 6)
                            {
                                NivelFisFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriFis) >= 6 && Convert.ToDouble(PromFinTriFis) < 8)
                                {
                                    NivelFisFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriFis) >= 8 && Convert.ToDouble(PromFinTriFis) < 10)
                                    {
                                        NivelFisFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriFis) == 10)
                                        {
                                            NivelFisFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelFisFin = " ";
                                        }
                                    }
                                }
                            }
                            //--------------------------------------Promedio final de Ed. Socioemocional trimestral--------------------------------
                            double PromFinSoc = 0;

                            PromFinSoc = Convert.ToDouble(PromTriSoc1) + Convert.ToDouble(PromTriSoc2) + Convert.ToDouble(PromTriSoc3);
                            PromFinSoc = PromFinSoc / 3;
                            PromFinTriSoc = PromFinSoc.ToString("0.#");//Promedio final de los trimestres de español

                            //Asignacion del nivel de los trimestres de español
                            if (Convert.ToDouble(PromFinTriSoc) >= 5 && Convert.ToDouble(PromFinTriSoc) < 6)
                            {
                                NivelSocFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriSoc) >= 6 && Convert.ToDouble(PromFinTriSoc) < 8)
                                {
                                    NivelSocFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriSoc) >= 8 && Convert.ToDouble(PromFinTriSoc) < 10)
                                    {
                                        NivelSocFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriSoc) == 10)
                                        {
                                            NivelSocFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelSocFin = " ";
                                        }
                                    }
                                }
                            }
                            //------------------------------------Promedio de materias desarrollo social tercer trimestre-------------------------------------------------
                            double PromDesFin = 0;

                            PromDesFin = Convert.ToDouble(PromFinTriArt) + Convert.ToDouble(PromFinTriFis) + Convert.ToDouble(PromFinTriSoc);
                            PromDesFin = PromDesFin / 3;
                            PromTriDesFin = PromDesFin.ToString("0.#");//Promedio del primer trimestre 

                            //Asignacion del nivel del primer trimestre 
                            if (Convert.ToDouble(PromTriDesFin) >= 5 && Convert.ToDouble(PromTriDesFin) < 6)
                            {
                                NivelPromDesFin = "I";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromTriDesFin) >= 6 && Convert.ToDouble(PromTriDesFin) < 8)
                                {
                                    NivelPromDesFin = "II";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromTriDesFin) >= 8 && Convert.ToDouble(PromTriDesFin) < 10)
                                    {
                                        NivelPromDesFin = "III";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromTriDesFin) == 10)
                                        {
                                            NivelPromDesFin = "IV";
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                            NivelPromDesFin = " ";
                                        }
                                    }
                                }
                            }

                            //-----------------------------si el nivel es mas alto en el desarrollo -------------------------------
                            if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                            {
                                Yes3 = "Yes";
                                Nou3 = "Off";
                            }
                            else
                            {
                                if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                                {
                                    Yes3 = "Yes";
                                    Nou3 = "Off";
                                }
                                else
                                {
                                    if (Convert.ToDouble(PromFinTriArt) > 5.9 && Convert.ToDouble(PromFinTriSoc) > 5.9)
                                    {
                                        Yes3 = "Yes";
                                        Nou3 = "Off";
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(PromFinTriSoc) > 5.9 && Convert.ToDouble(PromFinTriFis) > 5.9)
                                        {
                                            Yes3 = "Yes";
                                            Nou3 = "Off";
                                        }
                                        else
                                        {
                                            Yes3 = "Off";
                                            Nou3 = "Yes";
                                        }
                                    }
                                }
                            }
                            //--------------------Promedio de materias de formacion de ingles a conocimiento -------------------------------------------------
                            double PromMaterias = 0;

                            PromMaterias = Convert.ToDouble(PromFinTriIng) + Convert.ToDouble(PromFinTricn) + Convert.ToDouble(PromFinTriGeo) + Convert.ToDouble(PromFinTriHis) + Convert.ToDouble(PromFinTriCiv);
                            PromMaterias = PromMaterias / 5;
                            PromMateriasIC = PromMaterias.ToString("0.#");//Promedio del primer trimestre 

                            if (Convert.ToDouble(PromMateriasIC) >= 6)
                            {
                                Yes2 = "Yes";
                                Nou2 = "Off";
                            }
                            else
                            {
                                Yes2 = "Off";
                                Nou2 = "Yes";
                            }
                            //-----------------------------Numero final de Inasistencia-----------------------------------------------------
                            double SumaInasisFin = 0;
                            double SumaAsist = 0;
                            double PorcAsis = 0;

                            SumaInasisFin = Convert.ToDouble(SumaInasisTri1) + Convert.ToDouble(SumaInasisTri2) + Convert.ToDouble(SumaInasisTri3);
                            SumInasisTriFin = SumaInasisFin.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional

                            SumaAsist = 185 - SumaInasisFin;
                            SumAsisTriFin = SumaAsist.ToString("0.#");

                            PorcAsis = SumaAsist / 185;
                            PorcAsis = PorcAsis * 100;
                            PorcAsistencias = PorcAsis.ToString("0.##");
                        }
                        else
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                CalifJuni[i] = " ";
                            }
                        }
                        conn10.Close();

                        //Diagnostico-----------------------------------------------------------------
                        string CalifDig = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno1 + " AND `Mes` = 'Diagnostico'";

                        MySqlConnection conn11;
                        MySqlCommand com11;

                        conn11 = new MySqlConnection(conexion);
                        conn11.Open();

                        com11 = new MySqlCommand(CalifDig, conn11);

                        MySqlDataReader myreader11 = com11.ExecuteReader();

                        string[] CalifDiag = new string[11];
                        string PromDiag = " ";
                        string PromDiagGeneral = " ";

                        if (myreader11.HasRows) //Checa si las celdas estan vacias
                        {
                            int L = 0;
                            while (myreader11.Read())//Agrega calificaciones
                            {
                                double[] CalifDiagtemp = new double[11];
                                CalifDiagtemp[L] = Convert.ToDouble(myreader11["CalificacionMen"]);
                                CalifDiag[L] = CalifDiagtemp[L].ToString("0.#");
                                L++;
                            }

                            double PromDiagTemp = 0;

                            for (int i = 0; i < 9; i++)
                            {
                                PromDiagTemp = PromDiagTemp + Convert.ToDouble(CalifDiag[i]);
                            }

                            PromDiagTemp = PromDiagTemp / 9;
                            PromDiag = PromDiagTemp.ToString("0.#");//Promedio de diagnostico

                            double PromDiagGen = 0;

                            PromDiagGen = Convert.ToDouble(PromDiag) + Convert.ToDouble(CalifDiag[9]);
                            PromDiagGen = PromDiagGen / 2;
                            PromDiagGeneral = PromDiagGen.ToString("0.#"); //Promedio general de diagnostico
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                CalifDiag[i] = " ";
                            }
                        }
                        conn11.Close();
                        //------------------------------------------------------------------------------------------------
                        string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                        if (!Directory.Exists(folderPath))// pregunt si no existe
                        {
                            Directory.CreateDirectory(folderPath); // si no existe lo crea
                        }
                        string inputFile = Path.Combine(folderPath, "6_PRIMARIA_1819.pdf");
                        string outputFile = Path.Combine(folderPath, "BoletaExterna6.pdf");


                    }
                }

            }
        }

        // CAMBIOS!!!
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

        private void Septiembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(BoxGenero);
        }

        private void Octubre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox1);
        }

        private void Noviembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox2);
        }

        private void Diciembre_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox3);
        }

        private void Enero_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox4);
        }

        private void Febrero_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox5);
        }

        private void Marzo_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox6);
        }

        private void Abril_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox7);
        }

        private void Mayo_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox8);
        }

        private void Junio_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox9);
        }

        private void Diagnostico_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox10);
        }


    }
}
