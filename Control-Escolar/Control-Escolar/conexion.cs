using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Common;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Control_Escolar
{
    class conexion
    {
        public void Consulta(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se han Ingresado los datos correctamente");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }





        }


        public int Consul(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                MySqlDataReader myReader = com.ExecuteReader();
                myReader.Read();
                int resultado = Convert.ToInt32(myReader["COUNT(*)"]);
                return resultado;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return 0;
            }
            finally
            {


                conn.Close();

            }





        }

        public int Acceso(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                MySqlDataReader myReader = com.ExecuteReader();
                myReader.Read();
                int resultado = Convert.ToInt32(myReader["idAcceso"]);
                return resultado;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return 0;
            }
            finally
            {


                conn.Close();

            }





        }

        public void grupos(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                MySqlDataReader myReader = com.ExecuteReader();
                //myReader.Read();
                DataTable maestros = new DataTable();
                maestros.Load(myReader);
                List<string> nombre = new List<string>();
                List<string> apellidoP = new List<string>();
                List<string> apellidoM = new List<string>();
                foreach (DataRow row in maestros.Rows)
                {
                    nombre.Add(row["nombre"].ToString());
                    apellidoP.Add(row["apellidoP"].ToString());
                    apellidoM.Add(row["apellidoM"].ToString());
                }
                sesion.nombreU = nombre;
                sesion.apellidoP = apellidoP;
                sesion.apellidoM = apellidoM;
                return;


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }





        }

        public void insBitacora(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                //MessageBox.Show("Se ingresó la hora");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }

        }
        public void inspadres(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se ingresó datos padres");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }



        }

        public string Consultapadreshijos(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;


           


            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "0";

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                MySqlDataReader myReader = com.ExecuteReader();
                myReader.Read();
                string resultado = Convert.ToString(myReader["idpadres"]);
                return resultado;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return "0";
            }
            finally
            {


                conn.Close();

            }




        }

        public void insalumnos(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }


            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se ingresó datos alumno");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }



        }

        public void ElimarAlum(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se elimino alumno");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }

        }


    }





}

