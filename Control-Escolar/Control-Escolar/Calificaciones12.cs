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

            string[] CalifSept = new string[8];
            string PromSept = " ";
            string PromSeptGeneral = " ";

            if (myreader1.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader1.Read())//Agrega calificaciones
                {
                    double[] CalifSeptemp = new double[8];
                    CalifSeptemp[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                    CalifSept[L] = CalifSeptemp[L].ToString("0.#");
                    L++;
                }

                double PromSeptTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromSeptTemp = PromSeptTemp + Convert.ToDouble(CalifSept[i]);
                }

                PromSeptTemp = PromSeptTemp / 6;
                PromSept = PromSeptTemp.ToString("0.#"); //Promedio de septiembre

                double PromSeptGen = 0;

                PromSeptGen = Convert.ToDouble(PromSept) + Convert.ToDouble(CalifSept[6]);
                PromSeptGen = PromSeptGen / 2;
                PromSeptGeneral = PromSeptGen.ToString("0.#"); //Promedio general de semptiembre
            }
            else
            {
                for (int i = 0; i < 8; i++)
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

            string[] CalifOctu = new string[8];
            string PromOct = " ";
            string PromOctuGeneral = " ";

            if (myreader2.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader2.Read())//Agrega calificaciones
                {
                    double[] CalifOctutemp = new double[8];
                    CalifOctutemp[L] = Convert.ToDouble(myreader2["CalificacionMen"]);
                    CalifOctu[L] = CalifOctutemp[L].ToString("0.#");
                    L++;
                }

                double PromOctTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromOctTemp = PromOctTemp + Convert.ToDouble(CalifOctu[i]);
                }

                PromOctTemp = PromOctTemp / 6;
                PromOct = PromOctTemp.ToString("0.#"); //Promedio de octubre

                double PromOctGen = 0;

                PromOctGen = Convert.ToDouble(PromOct) + Convert.ToDouble(CalifOctu[6]);
                PromOctGen = PromOctGen / 2;
                PromOctuGeneral = PromOctGen.ToString("0.#"); //Promedio general de semptiembre
            }
            else
            {

                for (int i = 0; i < 8; i++)
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

            string[] CalifNovi = new string[8];
            string PromNov = " ";
            string PromTriSen1 = " ";
            string PromTriEsp1 = " ";
            string PromTriMat1 = " ";
            string PromTriIng1 = " ";
            string PromTriCon1 = " ";
            string PromTriArt1 = " ";
            string PromTriFis1 = " ";
            string PromTriSoc1 = " ";
            string SumaInasisTri1 = " ";
            string NivelPromSen = " ";
            string Nivel1 = " ";
            string NivelMat1 = " ";
            string NivelIng1 = " ";
            string NivelCon1 = " ";
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
                    double[] CalifNovitemp = new double[8];
                    CalifNovitemp[L] = Convert.ToDouble(myreader3["CalificacionMen"]);
                    CalifNovi[L] = CalifNovitemp[L].ToString("0.#");
                    L++;
                }

                double PromNovTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromNovTemp = PromNovTemp + Convert.ToDouble(CalifNovi[i]);
                }

                PromNovTemp = PromNovTemp / 6;
                PromNov = PromNovTemp.ToString("0.#");//Promedio de noviembre

                double PromNovGen = 0;

                PromNovGen = Convert.ToDouble(PromNov) + Convert.ToDouble(CalifNovi[6]);
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
                //-------------------------------------Conocimiento del medio--------------------------------------------------------
                double PromCon31 = 0;

                PromCon31 = Convert.ToDouble(CalifSept[3]) + Convert.ToDouble(CalifOctu[3]) + Convert.ToDouble(CalifNovi[3]);
                PromCon31 = PromCon31 / 3;
                PromTriCon1 = PromCon31.ToString("0.#");//Promedio del primer trimestre de Ciencias

                //Asignacion del nivel del primer trimestre de Ciencias
                if (Convert.ToDouble(PromTriCon1) >= 5 && Convert.ToDouble(PromTriCon1) < 6)
                {
                    NivelCon1 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCon1) >= 6 && Convert.ToDouble(PromTriCon1) < 8)
                    {
                        NivelCon1 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCon1) >= 8 && Convert.ToDouble(PromTriCon1) < 10)
                        {
                            NivelCon1 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCon1) == 10)
                            {
                                NivelCon1 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCon1 = " ";
                            }
                        }
                    }
                }
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt31 = 0;

                PromArt31 = Convert.ToDouble(CalifSept[4]) + Convert.ToDouble(CalifOctu[4]) + Convert.ToDouble(CalifNovi[4]);
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

                PromFis31 = Convert.ToDouble(CalifSept[5]) + Convert.ToDouble(CalifOctu[5]) + Convert.ToDouble(CalifNovi[5]);
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

                PromSoc31 = Convert.ToDouble(CalifSept[6]) + Convert.ToDouble(CalifOctu[6]) + Convert.ToDouble(CalifNovi[6]);
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

                SumaInasis31 = Convert.ToDouble(CalifSept[7]) + Convert.ToDouble(CalifOctu[7]) + Convert.ToDouble(CalifNovi[7]);
                SumaInasisTri1 = SumaInasis31.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
            }
            else
            {

                for (int i = 0; i < 8; i++)
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

            string[] CalifDici = new string[8];
            string PromDic = " ";
            string PromDiciGeneral = " ";

            if (myreader4.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader4.Read())//Agrega calificaciones
                {
                    double[] CalifDicitemp = new double[8];
                    CalifDicitemp[L] = Convert.ToDouble(myreader4["CalificacionMen"]);
                    CalifDici[L] = CalifDicitemp[L].ToString("0.#");
                    L++;
                }

                double PromDicTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromDicTemp = PromDicTemp + Convert.ToDouble(CalifDici[i]);
                }

                PromDicTemp = PromDicTemp / 6;
                PromDic = PromDicTemp.ToString("0.#");//Promedio de diciembre

                double PromDiciGen = 0;

                PromDiciGen = Convert.ToDouble(PromDic) + Convert.ToDouble(CalifDici[6]);
                PromDiciGen = PromDiciGen / 2;
                PromDiciGeneral = PromDiciGen.ToString("0.#"); //Promedio general de diciembre
            }
            else
            {

                for (int i = 0; i < 8; i++)
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

            string[] CalifEner = new string[8];
            string PromEne = " ";
            string PromEnerGeneral = " ";

            if (myreader5.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader5.Read())//Agrega calificaciones
                {
                    double[] CalifEnertemp = new double[8];
                    CalifEnertemp[L] = Convert.ToDouble(myreader5["CalificacionMen"]);
                    CalifEner[L] = CalifEnertemp[L].ToString("0.#");
                    L++;
                }

                double PromEneTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromEneTemp = PromEneTemp + Convert.ToDouble(CalifEner[i]);
                }

                PromEneTemp = PromEneTemp / 6;
                PromEne = PromEneTemp.ToString("0.#");//Promedio de enero

                double PromEnerGen = 0;

                PromEnerGen = Convert.ToDouble(PromEne) + Convert.ToDouble(CalifEner[6]);
                PromEnerGen = PromEnerGen / 2;
                PromEnerGeneral = PromEnerGen.ToString("0.#"); //Promedio general de enero
            }
            else
            {

                for (int i = 0; i < 8; i++)
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

            string[] CalifFebr = new string[8];
            string PromFeb = " ";
            string PromTriEsp2 = " ";
            string PromTriMat2 = " ";
            string PromTriIng2 = " ";
            string PromTriCon2 = " ";
            string PromTriArt2 = " ";
            string PromTriFis2 = " ";
            string PromTriSoc2 = " ";
            string PromTriSen2 = " ";
            string SumaInasisTri2 = " ";
            string Nivel2 = " ";
            string NivelMat2 = " ";
            string NivelIng2 = " ";
            string NivelCon2 = " ";
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
                    double[] CalifFebrtemp = new double[8];
                    CalifFebrtemp[L] = Convert.ToDouble(myreader6["CalificacionMen"]);
                    CalifFebr[L] = CalifFebrtemp[L].ToString("0.#");
                    L++;
                }

                double PromFebTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromFebTemp = PromFebTemp + Convert.ToDouble(CalifFebr[i]);
                }

                PromFebTemp = PromFebTemp / 6;
                PromFeb = PromFebTemp.ToString("0.#");//Promedio de febrero

                double PromFebrGen = 0;

                PromFebrGen = Convert.ToDouble(PromFeb) + Convert.ToDouble(CalifFebr[6]);
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
                //-------------------------------------Conocimiento del medio--------------------------------------------------------
                double PromCon32 = 0;

                PromCon32 = Convert.ToDouble(CalifDici[3]) + Convert.ToDouble(CalifEner[3]) + Convert.ToDouble(CalifFebr[3]);
                PromCon32 = PromCon32 / 3;
                PromTriCon2 = PromCon32.ToString("0.#");//Promedio del primer trimestre de Ciencias

                //Asignacion del nivel del primer trimestre de Ciencias
                if (Convert.ToDouble(PromTriCon2) >= 5 && Convert.ToDouble(PromTriCon2) < 6)
                {
                    NivelCon2 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCon2) >= 6 && Convert.ToDouble(PromTriCon2) < 8)
                    {
                        NivelCon2 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCon2) >= 8 && Convert.ToDouble(PromTriCon2) < 10)
                        {
                            NivelCon2 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCon2) == 10)
                            {
                                NivelCon2 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCon2 = " ";
                            }
                        }
                    }
                }
                
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt32 = 0;

                PromArt32 = Convert.ToDouble(CalifDici[4]) + Convert.ToDouble(CalifEner[4]) + Convert.ToDouble(CalifFebr[4]);
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

                PromFis32 = Convert.ToDouble(CalifDici[5]) + Convert.ToDouble(CalifEner[5]) + Convert.ToDouble(CalifFebr[5]);
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

                PromSoc32 = Convert.ToDouble(CalifDici[6]) + Convert.ToDouble(CalifEner[6]) + Convert.ToDouble(CalifFebr[6]);
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

                SumaInasis32 = Convert.ToDouble(CalifDici[7]) + Convert.ToDouble(CalifEner[7]) + Convert.ToDouble(CalifFebr[7]);
                SumaInasisTri2 = SumaInasis32.ToString("0.#");//Promedio del primer trimestre de Ed. Socioemocional
            }
            else
            {
                for (int i = 0; i < 8; i++)
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

            string[] CalifMarz = new string[8];
            string PromMar = " ";
            string PromMarzGeneral = " ";

            if (myreader7.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader7.Read())//Agrega calificaciones
                {
                    double[] CalifMarztemp = new double[8];
                    CalifMarztemp[L] = Convert.ToDouble(myreader7["CalificacionMen"]);
                    CalifMarz[L] = CalifMarztemp[L].ToString("0.#");
                    L++;
                }

                double PromMarTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromMarTemp = PromMarTemp + Convert.ToDouble(CalifMarz[i]);
                }

                PromMarTemp = PromMarTemp / 6;
                PromMar = PromMarTemp.ToString("0.#");//Promedio de  marzo

                double PromMarzGen = 0;

                PromMarzGen = Convert.ToDouble(PromMar) + Convert.ToDouble(CalifMarz[6]);
                PromMarzGen = PromMarzGen / 2;
                PromMarzGeneral = PromMarzGen.ToString("0.#"); //Promedio general de marzo

            }
            else
            {
                for (int i = 0; i < 8; i++)
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

            string[] CalifAbri = new string[8];
            string PromAbr = " ";
            string PromAbriGeneral = " ";

            if (myreader8.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader8.Read())//Agrega calificaciones
                {
                    double[] CalifAbritemp = new double[8];
                    CalifAbritemp[L] = Convert.ToDouble(myreader8["CalificacionMen"]);
                    CalifAbri[L] = CalifAbritemp[L].ToString("0.#");
                    L++;
                }

                double PromAbrTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromAbrTemp = PromAbrTemp + Convert.ToDouble(CalifAbri[i]);
                }

                PromAbrTemp = PromAbrTemp / 6;
                PromAbr = PromAbrTemp.ToString("0.#");//Promedio de abril

                double PromAbriGen = 0;

                PromAbriGen = Convert.ToDouble(PromAbr) + Convert.ToDouble(CalifAbri[6]);
                PromAbriGen = PromAbriGen / 2;
                PromAbriGeneral = PromAbriGen.ToString("0.#"); //Promedio general de abril

            }
            else
            {
                for (int i = 0; i < 8; i++)
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

            string[] CalifMayo = new string[8];
            string PromMay = " ";
            string PromMayoGeneral = " ";

            if (myreader9.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader9.Read())//Agrega calificaciones
                {
                    double[] CalifMayotemp = new double[8];
                    CalifMayotemp[L] = Convert.ToDouble(myreader9["CalificacionMen"]);
                    CalifMayo[L] = CalifMayotemp[L].ToString("0.#");
                    L++;
                }

                double PromMayTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromMayTemp = PromMayTemp + Convert.ToDouble(CalifMayo[i]);
                }

                PromMayTemp = PromMayTemp / 6;
                PromMay = PromMayTemp.ToString("0.#");//Promedio de mayo

                double PromMayoGen = 0;

                PromMayoGen = Convert.ToDouble(PromMay) + Convert.ToDouble(CalifMayo[6]);
                PromMayoGen = PromMayoGen / 2;
                PromMayoGeneral = PromMayoGen.ToString("0.#"); //Promedio general de mayo
            }
            else
            {
                for (int i = 0; i < 8; i++)
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

            string[] CalifJuni = new string[8];
            string PromJun = " ";
            string PromTriEsp3 = " ";
            string PromTriMat3 = " ";
            string PromTriIng3 = " ";
            string PromTriCon3 = " ";
            string PromTriArt3 = " ";
            string PromTriFis3 = " ";
            string PromTriSoc3 = " ";
            string SumaInasisTri3 = " ";
            string Nivel3 = " ";
            string NivelMat3 = " ";
            string NivelIng3 = " ";
            string NivelCon3 = " ";
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
            string PromFinTriCon = " ";
            string NivelConFin = " ";
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
                    double[] CalifJunitemp = new double[8];
                    CalifJunitemp[L] = Convert.ToDouble(myreader10["CalificacionMen"]);
                    CalifJuni[L] = CalifJunitemp[L].ToString("0.#");
                    L++;
                }

                double PromJunTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromJunTemp = PromJunTemp + Convert.ToDouble(CalifJuni[i]);
                }

                PromJunTemp = PromJunTemp / 6;
                PromJun = PromJunTemp.ToString("0.#");//Promedio de junio

                double PromJuniGen = 0;

                PromJuniGen = Convert.ToDouble(PromJun) + Convert.ToDouble(CalifJuni[6]);
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
                //-------------------------------------Conocimiento del medio--------------------------------------------------------
                double PromCon33 = 0;

                PromCon33 = Convert.ToDouble(CalifMarz[3]) + Convert.ToDouble(CalifAbri[3]) + Convert.ToDouble(CalifMayo[3]) + Convert.ToDouble(CalifJuni[3]);
                PromCon33 = PromCon33 / 4;
                PromTriCon3 = PromCon33.ToString("0.#");//Promedio del tercer trimestre de Ciencias

                //Asignacion del nivel del tercer trimestre de Ciencias
                if (Convert.ToDouble(PromTriCon3) >= 5 && Convert.ToDouble(PromTriCon3) < 6)
                {
                    NivelCon3 = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromTriCon3) >= 6 && Convert.ToDouble(PromTriCon3) < 8)
                    {
                        NivelCon3 = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromTriCon3) >= 8 && Convert.ToDouble(PromTriCon3) < 10)
                        {
                            NivelCon3 = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromTriCon3) == 10)
                            {
                                NivelCon3 = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelCon3 = " ";
                            }
                        }
                    }
                }
               
                //-------------------------------------Artes--------------------------------------------------------
                double PromArt33 = 0;

                PromArt33 = Convert.ToDouble(CalifMarz[4]) + Convert.ToDouble(CalifAbri[4]) + Convert.ToDouble(CalifMayo[4]) + Convert.ToDouble(CalifJuni[4]);
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

                PromFis33 = Convert.ToDouble(CalifMarz[5]) + Convert.ToDouble(CalifAbri[5]) + Convert.ToDouble(CalifMayo[5]) + Convert.ToDouble(CalifJuni[5]);
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

                PromSoc33 = Convert.ToDouble(CalifMarz[6]) + Convert.ToDouble(CalifAbri[6]) + Convert.ToDouble(CalifMayo[6]) + Convert.ToDouble(CalifJuni[6]);
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

                SumaInasis33 = Convert.ToDouble(CalifMarz[7]) + Convert.ToDouble(CalifAbri[7]) + Convert.ToDouble(CalifMayo[7]) + Convert.ToDouble(CalifJuni[7]);
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
                //--------------------------------------Promedio final de Conocimiento del medio trimestral--------------------------------
                double PromFinCon = 0;

                PromFinCon = Convert.ToDouble(PromTriCon1) + Convert.ToDouble(PromTriCon2) + Convert.ToDouble(PromTriCon3);
                PromFinCon = PromFinCon / 3;
                PromFinTriCon = PromFinCon.ToString("0.#");//Promedio final de los trimestres de español

                //Asignacion del nivel de los trimestres de español
                if (Convert.ToDouble(PromFinTriCon) >= 5 && Convert.ToDouble(PromFinTriCon) < 6)
                {
                    NivelConFin = "I";
                }
                else
                {
                    if (Convert.ToDouble(PromFinTriCon) >= 6 && Convert.ToDouble(PromFinTriCon) < 8)
                    {
                        NivelConFin = "II";
                    }
                    else
                    {
                        if (Convert.ToDouble(PromFinTriCon) >= 8 && Convert.ToDouble(PromFinTriCon) < 10)
                        {
                            NivelConFin = "III";
                        }
                        else
                        {
                            if (Convert.ToDouble(PromFinTriCon) == 10)
                            {
                                NivelConFin = "IV";
                            }
                            else
                            {
                                MessageBox.Show("No se encuentra en ningun nivel de desempeño");
                                NivelConFin = " ";
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
                for (int i = 0; i < 8; i++)
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

            string[] CalifDiag = new string[8];
            string PromDiag = " ";
            string PromDiagGeneral = " ";

            if (myreader11.HasRows) //Checa si las celdas estan vacias
            {
                int L = 0;
                while (myreader11.Read())//Agrega calificaciones
                {
                    double[] CalifDiagtemp = new double[8];
                    CalifDiagtemp[L] = Convert.ToDouble(myreader11["CalificacionMen"]);
                    CalifDiag[L] = CalifDiagtemp[L].ToString("0.#");
                    L++;
                }

                double PromDiagTemp = 0;

                for (int i = 0; i < 6; i++)
                {
                    PromDiagTemp = PromDiagTemp + Convert.ToDouble(CalifDiag[i]);
                }

                PromDiagTemp = PromDiagTemp / 6;
                PromDiag = PromDiagTemp.ToString("0.#");//Promedio de diagnostico

                double PromDiagGen = 0;

                PromDiagGen = Convert.ToDouble(PromDiag) + Convert.ToDouble(CalifDiag[6]);
                PromDiagGen = PromDiagGen / 2;
                PromDiagGeneral = PromDiagGen.ToString("0.#"); //Promedio general de diagnostico
            }
            else
            {
                for (int i = 0; i < 8; i++)
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
            FileStream stream = new FileStream(folderPath + "Boleta-Interna12.pdf", FileMode.Create);
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

            PdfPCell cell43 = new PdfPCell(new Phrase("ALUMNO:   " + Apellidop + "     " + Apellidom + "     " + nombre + "     CURP: " + sesion.Curp));
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

            PdfPCell cell6 = new PdfPCell(new Phrase("CONOCIMIENTO DEL MEDIO", cuerpo));
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
            table.AddCell("" + PromTriCon1);
            table.AddCell("" + NivelCon1);
            table.AddCell("" + PromTriCon2);
            table.AddCell("" + NivelCon2);
            table.AddCell("" + PromTriCon3);
            table.AddCell("" + NivelCon3);
            table.AddCell("" + PromFinTriCon);
            table.AddCell("" + NivelConFin);

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            //mes = materialTabControl1.SelectedTab.Name;
            //    switch (mes)
            //    {
            //        case "Septiembre":
            //            {
            //                calisep();
            //                MessageBox.Show("Calificaciones  septiembre registradas  con exito");
            //            }
            //            break;

            //        case "Octubre":
            //            {
            //                caliOct();
            //                MessageBox.Show("Calificaciones  octubre registradas con exito");
            //            }
            //            break;

            //        case "Noviembre":
            //            {
            //                caliNov();
            //                MessageBox.Show("Calificaciones  noviembre registradas con exito");
            //            }
            //            break;

            //        case "Diciembre":
            //            {
            //                caliDic();
            //                MessageBox.Show("Calificaciones  diciembre registradas con exito");
            //            }
            //            break;

            //        case "Enero":
            //            {
            //                caliEnero();
            //                MessageBox.Show("Calificaciones  Enero registradas con exito");
            //            }
            //            break;
            //        case "Febrero":
            //            {
            //                caliFebrero();
            //                MessageBox.Show("Calificaciones  febrero registradas con exito");
            //            }
            //            break;

            //        case "Marzo":
            //            {
            //                caliMarzo();
            //                MessageBox.Show("Calificaciones  Marzo registradas con exito");
            //            }
            //            break;

            //        case "Abril":
            //            {
            //                caliAbril();
            //                MessageBox.Show("Calificaciones  Abril registradas con exito");
            //            }
            //            break;

            //        case "Mayo":
            //            {
            //                caliMayo();
            //                MessageBox.Show("Calificaciones  Mayo registradas con exito");
            //            }
            //            break;

            //        case "Junio":
            //            {
            //                caliJunio();
            //                MessageBox.Show("Calificaciones  Junio registradas con exito");
            //            }
            //            break;


            //        case "Diagnostico":
            //            {
            //                caliDiagnostico();
            //                MessageBox.Show("Calificaciones  Diagnostico registradas con exito");
            //            }
            //            break;

            //    }
        }

        

        //public void calisep()
        //{

        //    Español = EspDiag.SelectedItem.ToString();
        //    Matematicas = EspSep.SelectedItem.ToString();
        //    Ingless = EspOct.SelectedItem.ToString();
        //    Conocimiento = Espnov.SelectedItem.ToString();
        //    Artess = EspMarz.SelectedItem.ToString();
        //    EducacionF = Espmay.SelectedItem.ToString();
        //    Edsocio = Espabril.SelectedItem.ToString();
        //    Inasistencias = cmbsepina.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        //}


        //public void caliOct()
        //{
        //    Español = cmbEspañol.SelectedItem.ToString();
        //    Matematicas = cmbOctubreMate.SelectedItem.ToString();
        //    Ingless = cmbOctubreIngles.SelectedItem.ToString();
        //    Conocimiento = cmbOctubreCiencias.SelectedItem.ToString();
        //    Artess = cmboctubreArt.SelectedItem.ToString();
        //    EducacionF = cmbOctubreEdFisica.SelectedItem.ToString();
        //    Edsocio = cmbOctubreEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbOctubreIna.SelectedItem.ToString();

        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}


        //public void caliNov()
        //{
        //    Español = cmbNovEspañol.SelectedItem.ToString();
        //    Matematicas = cmbNovmate.SelectedItem.ToString();
        //    Ingless = cmbNovIngles.SelectedItem.ToString();
        //    Conocimiento = cmbNovCiencias.SelectedItem.ToString();
        //    Artess = cmbNovArtes.SelectedItem.ToString();
        //    EducacionF = cmbNovEdFisi.SelectedItem.ToString();
        //    Edsocio = cmbNovEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbNovIna.SelectedItem.ToString();

        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}


        //public void caliDic()
        //{
        //    Español = cmbDicEspañol.SelectedItem.ToString();
        //    Matematicas = cmbDicMate.SelectedItem.ToString();
        //    Ingless = cmbDicIngles.SelectedItem.ToString();
        //    Conocimiento = cmbDicCiencias.SelectedItem.ToString();
        //    Artess = cmbDicArtes.SelectedItem.ToString();
        //    EducacionF = cmbDicEdFisica.SelectedItem.ToString();
        //    Edsocio = cmbDicEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbDicInasis.SelectedItem.ToString();

        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}


        //public void caliEnero()
        //{
        //    Español = cmbEneroEspañol.SelectedItem.ToString();
        //    Matematicas = cmbEneroMate.SelectedItem.ToString();
        //    Ingless = cmbEneroIngles.SelectedItem.ToString();
        //    Conocimiento = cmbEneroCiencias.SelectedItem.ToString();
        //    Artess = cmbEneroArtess.SelectedItem.ToString();
        //    EducacionF = cmbEneroEdfisica.SelectedItem.ToString();
        //    Edsocio = cmbEneroEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbEneroIna.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}

        //public void caliFebrero()
        //{
        //    Español = cmbFebreroEspañol.SelectedItem.ToString();
        //    Matematicas = cmbFebreroMate.SelectedItem.ToString();
        //    Ingless = cmbfebreroIngles.SelectedItem.ToString();
        //    Conocimiento = cmbFebreroCiencias.SelectedItem.ToString();
        //    Artess = cmbFebreroArtess.SelectedItem.ToString();
        //    EducacionF = cmbFebreroEdfisica.SelectedItem.ToString();
        //    Edsocio = cmbFebreroEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbfebreroIna.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}


        //public void caliMarzo()
        //{

        //    Español = cmbmarzoEspañol.SelectedItem.ToString();
        //    Matematicas = cmbMarzoMate.SelectedItem.ToString();
        //    Ingless = cmbMarzoIngles.SelectedItem.ToString();
        //    Conocimiento = cmbMarzoCiencias.SelectedItem.ToString();
        //    Artess = cmbMarzoArtess.SelectedItem.ToString();
        //    EducacionF = cmbMarzoEdFisica.SelectedItem.ToString();
        //    Edsocio = cmbMarzoEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbmarzoina.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}

        //public void caliAbril()
        //{
        //    Español = cmbAbrilEspañol.SelectedItem.ToString();
        //    Matematicas = cmbAbrilmate.SelectedItem.ToString();
        //    Ingless = cmbAbrilIngles.SelectedItem.ToString();
        //    Conocimiento = cmbAbrilCiencias.SelectedItem.ToString();
        //    Artess = cmbAbrilArtess.SelectedItem.ToString();
        //    EducacionF = cmbAbrilEdfisica.SelectedItem.ToString();
        //    Edsocio = cmbAbrilEdsocio.SelectedItem.ToString();
        //    Inasistencias = cmbAbrilIna.SelectedItem.ToString();

        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}
        //public void caliMayo()
        //{
        //    Español = cmbmayoEspañol.SelectedItem.ToString();
        //    Matematicas = cmbMayoMate.SelectedItem.ToString();
        //    Ingless = cmbMayoIngles.SelectedItem.ToString();
        //    Conocimiento = cmbMayoCiencias.SelectedItem.ToString();
        //    Artess = cmbMayoArtes.SelectedItem.ToString();
        //    Edsocio = cmbMayoEdsocio.SelectedItem.ToString();
        //    EducacionF = cmbMayoEdfisica.SelectedItem.ToString();
        //    Inasistencias = cmbMayoIna.SelectedItem.ToString();

        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}

        //public void caliJunio()
        //{
        //    Español = cmbDJunioEspañol.SelectedItem.ToString();
        //    Matematicas = cmbJuniomate.SelectedItem.ToString();
        //    Ingless = cmbJunioingless.SelectedItem.ToString();
        //    Conocimiento = cmbJunioingless.SelectedItem.ToString();
        //    Artess = cmbJunioArtess.SelectedItem.ToString();
        //    EducacionF = cmbJunioEdFis.SelectedItem.ToString();
        //    Edsocio = cmbJunioEdsocioe.SelectedItem.ToString();
        //    Inasistencias = cmbJunioinasis.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}

        //public void caliDiagnostico()
        //{
        //    Español = cmbdiagespañol.SelectedItem.ToString();
        //    Matematicas = cmbdiagmate.SelectedItem.ToString();
        //    Ingless = cmbdiagingles.SelectedItem.ToString();
        //    Conocimiento = cmbdiagciencias.SelectedItem.ToString();
        //    Artess = cmbdiagartes.SelectedItem.ToString();
        //    EducacionF = cmbdiagedfisica.SelectedItem.ToString();
        //    Edsocio = cmbdiagedsocio.SelectedItem.ToString();
        //    Inasistencias = cmbdiaginasis.SelectedItem.ToString();


        //    materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
        //    materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
        //    materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
        //    materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
        //    materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
        //    materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
        //    materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
        //    materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        //}

        private void bindingNavigator2_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

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

    }
}
